using Leopotam.Ecs;
using UnityEngine;

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
                .Add(new LookAtCameraSystem())
                .Add(new GameAreaSystem())
                .Add(new AgentAnimationSystem())
                .Add(new RespawnSystem())
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
}