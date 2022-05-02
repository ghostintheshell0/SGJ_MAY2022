using Leopotam.Ecs;
using UnityEngine;
using DG.Tweening;

namespace Game
{
    public class FinalCutSceneSystem : IEcsRunSystem
    {
        private readonly EcsFilter<FinalCutsceneComponent> _filter = default;
        private readonly EcsWorld _world = default;
        private readonly SceneData _sceneData = default;
    
        public void Run()
        {
            #if UNITY_EDITOR
            if(Input.GetKeyDown(_sceneData.FinalCutScene.DebugKey))
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
                    cutsceneData.Npc.Obstacle.enabled = false;
                    cutsceneData.Npc.Agent.enabled = true;
                    cutsceneData.Npc.Agent.SetDestination(cutsceneData.HousePoint.position);
                    cutsceneData.House.Trigger.enabled = false;
                    cutsceneData.CutSceneCamera.enabled = true;
                    cutsceneData.Npc.TriggerCollider.enabled = false;
                    cutsceneData.Npc.Entity.Del<LookAtComponent>();
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
                        cmd.ThirdPart = true;
                        cmd.Delay = cutsceneData.ThirtdPartDuration;
                        cutsceneData.HouseAnimator.SetBool(AniamtionNames.OpenRoof, true);
                        cutsceneData.Player.gameObject.SetActive(false);
                        cutsceneData.Npc.gameObject.SetActive(false);
                        continue;
                    }
                    else if(cmd.FourthPart == false)
                    {
                        cmd.FourthPart = true;
                        cmd.Delay = cutsceneData.FourthPartDuration;
                        cutsceneData.Traktor.DOMove(cutsceneData.TargetHeight.position, cutsceneData.HeightDuration);
                        continue;
                    }
                    else
                    {
                        _sceneData.EndGameScreen.alpha = 0;
                        _sceneData.EndGameScreen.gameObject.SetActive(true);
                        cutsceneData.TraktorBody.velocity = cutsceneData.Traktor.transform.forward * cutsceneData.TraktorSpeed;
                        _sceneData.EndGameScreen.DOFade(1f, _sceneData.EndGameScreenFadeDuration);
                        _filter.GetEntity(i).Del<FinalCutsceneComponent>();
                    }
                }
            }
        }
    }
}