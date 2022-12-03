using Leopotam.Ecs;
using LeopotamGroup.Globals;
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
            InitServices();

            _systems
                .Add(new InitSystem())
                .Add(new SpawnPlayerSystem())
                .Add(new SpawnCrapSystem())
                .Add(new FixCrapSpawnSystem())
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
                .Add(new ColliderUnlockSysytem())
                .Add(new CutSceneSystem())
                .Add(new EasterDancingSystem())
                .Add(new ChangeSceneSystem())
                .Add(new IgnoreTriggersSystem())
                .Add(new FollowSystem())
                .Add(new FootStepsEmmiterSystem())
                .Add(new EndGameSystem())
                .Add(new SkipCutSceneSystem())
                    
                .Inject(_sceneData)
                .Inject(_staticData)
                .Inject(_runtimeData)
                .Inject(Service<UI>.Get())
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

        private void InitServices()
        {
            Service<StaticData>.Set(_staticData);
            Service<SceneData>.Set(_sceneData);

            var audioManager = InitServiceFromPrefab<AudioManager>(_staticData.AudioManagerPrefab);
            var ui = InitServiceFromPrefab<UI>(_staticData.UIPrefab);
        //    var player = InitServiceFromPrefab<Character>(_staticData.PlayerPrefab);
            
            var runtimeData = Service<RuntimeData>.Get();
            if(runtimeData == default)
            {
                Service<RuntimeData>.Set(_runtimeData);
            }
            else
            {
                if(_runtimeData.IsNewGame)
                {
                    Restart();
                }
                else
                {
                    _runtimeData = runtimeData;
                }
                
            }

          //  _sceneData.Player = player;
            
        }

    //TODO переписать
        private void Restart()
        {
            var audioManager = Service<AudioManager>.Get();
            var ui = Service<UI>.Get();

            audioManager.Music.volume = audioManager.defaultVolume;
            audioManager.Wind.volume = audioManager.defaultVolume;
            ui.EndGameScreen.alpha = 0;
            ui.EndGameScreen.gameObject.SetActive(false);
            _runtimeData.IsNewGame = false;
            _runtimeData.IsEnd = false;
            _runtimeData.DeliveredItems.Clear();
        }

        private T InitServiceFromPrefab<T>(T prefab) where T : Component
        {
            var service = Service<T>.Get();
            if(service == default)
            {
                service = Instantiate(prefab);
                DontDestroyOnLoad(service);
                Service<T>.Set(service);
            }

            return service;
        }
    }
}