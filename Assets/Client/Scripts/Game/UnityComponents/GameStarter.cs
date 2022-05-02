using Leopotam.Ecs;
using UnityEngine;
using DG.Tweening;

namespace Game
{
    public class GameStarter : MonoBehaviour
    {
        public SceneData _sceneData = default;
        public StaticData _staticData = default;
        public RuntimeData _runtimeData = default;

        EcsWorld _world;
        EcsSystems _systems;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _systems
                .Add(new InitSystem())
                .Add(new SpawnCrapSystem())
                .Add(new RaycastsSystem())
                .Add(new PlayerInputSystem())
                .Add(new PickCrapSystem())
                .Add(new EnterNpcSystem())
                .Add(new HideObjectWithDelaySystem())
                .Add(new LookAtSystem())
                .Add(new GameAreaSystem())
                .Add(new AgentAnimationSystem())
                .Add(new RespawnSystem())
                .Add(new ProgressionSystem())
                .Add(new FinalCutSceneSystem())
                .Inject(_sceneData)
                .Inject(_staticData)
                .Inject(_runtimeData)
                .Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            if(_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }

    public class FinalCutSceneSystem : IEcsRunSystem
    {
        private readonly EcsFilter<FinalCutsceneComponent> _filter = default;
        private readonly EcsWorld _world = default;
        private readonly SceneData _sceneData = default;
    
        public void Run()
        {
            #if UNITY_EDITOR
            if(Input.GetKeyDown(KeyCode.O))
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
                        cutsceneData.TraktorBody.velocity = cutsceneData.Traktor.transform.forward * cutsceneData.TraktorSpeed;
                        _filter.GetEntity(i).Del<FinalCutsceneComponent>();
                    }
                }
            }
        }
    }
    
    public struct FinalCutsceneComponent
    {
        public bool FirstStep;
        public bool SecondStep;
        public bool ThirdPart;
        public bool FourthPart;
        public bool FifthPart;
        public float Delay;
    }
}