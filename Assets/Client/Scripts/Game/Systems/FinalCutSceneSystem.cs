using Leopotam.Ecs;
using UnityEngine;
using DG.Tweening;
using Extensions;
using LeopotamGroup.Globals;

namespace Game
{
    public class FinalCutSceneSystem : IEcsRunSystem
    {
        private readonly EcsFilter<FinalCutsceneComponent> _filter = default;
        private readonly EcsWorld _world = default;
        private readonly SceneData _sceneData = default;
        private readonly UI _ui = default;
    
        public void Run()
        {
            #if UNITY_EDITOR
            if(_sceneData.FinalCutScene != default && Input.GetKeyDown(_sceneData.FinalCutScene.DebugKey))
            {
                _world.NewEntity().Get<FinalCutsceneComponent>();
            }
            #endif

            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);

                var cutsceneData = _sceneData.FinalCutScene;

                if(cmd.FirstStep == false)
                {
                    _sceneData.CinemachineBrain.m_DefaultBlend.m_Time = cutsceneData.Camera1BlendDuration;
                    
                    cutsceneData.CutSceneCamera.enabled = true;
                    cutsceneData.Npc.ReadyForMove();
                    cutsceneData.Npc.Agent.SetDestination(cutsceneData.HousePoint.position);
                    cmd.FirstStep = true;
                    cmd.Delay = cutsceneData.FirstPartDuration;
                    cutsceneData.Sun.DORotate(cutsceneData.SunRotation, cutsceneData.SunRotationDuration);
                    _world.NewEntity().Get<LockInputComponent>();
                    continue;
                }

                cmd.Delay -= Time.deltaTime;
                if(cmd.Delay <= 0)
                {
                    if(cmd.SecondStep == false)
                    {
                        cmd.SecondStep = true;
                        cmd.Delay = cutsceneData.SecondPartDuration;
                        cutsceneData.Player.Agent.SetDestination(cutsceneData.HousePoint.position);

                        continue;
                    }
                    else if(cmd.ThirdPart == false)
                    {
                        _sceneData.CinemachineBrain.m_DefaultBlend.m_Time = cutsceneData.Camera2BlendDuration;
                        cutsceneData.CutSceneCamera.enabled = false;
                        cutsceneData.CutSceneCamera2.enabled = true;
                        cmd.ThirdPart = true;
                        cmd.Delay = cutsceneData.ThirtdPartDuration;
                        cutsceneData.HouseAnimator.SetBool(AniamtionNames.OpenRoof, true);
                        cutsceneData.Player.gameObject.SetActive(false);
                        cutsceneData.Npc.gameObject.SetActive(false);
                        var audioManager = Service<AudioManager>.Get();
                        audioManager.Wind.DOFade(0, cutsceneData.ThirtdPartDuration);
                        continue;
                    }
                    else if(cmd.FourthPart == false)
                    {
                        cmd.FourthPart = true;
                        cmd.Delay = cutsceneData.FourthPartDuration;
                        var height = cutsceneData.Traktor.position.y + cutsceneData.TraktorHeight;
                        cutsceneData.Traktor.DOMoveY(height, cutsceneData.HeightDuration);
                        continue;
                    }
                    else if(cmd.FifthPart == false)
                    {
                        cmd.FifthPart = true;
                        cmd.Delay = cutsceneData.FifthPartDuration;
                        _ui.EndGameScreen.alpha = 0;
                        cutsceneData.TraktorBody.velocity = cutsceneData.Traktor.transform.forward * cutsceneData.TraktorSpeed;
                        var angle = cutsceneData.Traktor.rotation.eulerAngles;
                        angle.x += cutsceneData.TraktorFlyAngle;
                        cutsceneData.Traktor.DORotate(angle, cutsceneData.TraktorFlyAngleDuration);
                        continue;
                    }
                    else
                    {
                        _ui.EndGameScreen.gameObject.SetActive(true);
                        _ui.EndGameScreen.DOFade(1f, _ui.EndGameScreenFadeDuration);
                        _ui.Developers.text = _ui.DevelopersVariations.GetRandom();
                        _filter.GetEntity(i).Del<FinalCutsceneComponent>();
                    }
                }
            }
        }
    }
}