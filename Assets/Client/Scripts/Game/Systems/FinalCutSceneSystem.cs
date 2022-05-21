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
        private readonly EcsFilter<NpcComponent> _npcs = default;
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

                if(cmd.ZeroStep == false)
                {
                    foreach(var k in _npcs)
                    {
                        ref var npc = ref _npcs.Get1(k);
                        npc.View.gameObject.SetActive(false);
                    }
                    
                    cmd.ZeroStep = true;
                    cmd.Delay = cutsceneData.ZeroPartDuration;
                    cutsceneData.CutSceneCamera3.enabled = true;
                    _sceneData.CinemachineBrain.m_DefaultBlend.m_Time = cutsceneData.Camera2BlendDuration;
                    continue;
                }

                cmd.Delay -= Time.deltaTime;
                if(cmd.Delay <= 0)
                {
                    if(cmd.FirstStep == false)
                    {
                        cmd.FirstStep = true;
                        _ui.ChangeSceneFade.DOFade(0, _ui.ChangeSceneFadeDuration * 3);
                        cutsceneData.CutSceneCamera.enabled = true;
                        cmd.Delay = cutsceneData.FirstPartDuration;
                        cutsceneData.Sun.DORotate(cutsceneData.SunRotation, cutsceneData.SunRotationDuration);
                        _world.NewEntity().Get<LockInputComponent>();
                        continue;
                    }
                    else if(cmd.SecondStep == false)
                    {
                        cmd.SecondStep = true;
                        cmd.Delay = cutsceneData.SecondPartDuration;

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
                        cutsceneData.CutSceneCamera4.enabled = true;
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