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

            if(_runtimeData.IsEnd)
            {
                EndGame();
            }
            else
            {
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
                    .Add(new EasterDancingSystem())
                    .Add(new ChangeSceneSystem())
                    .Add(new SpawnPlayerSystem())
                    .Add(new IgnoreTriggersSystem())
                    .Add(new FollowSustem());
            }

            _systems
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
            var runtimeData = Service<RuntimeData>.Get();
            if(runtimeData == default)
            {
                Service<RuntimeData>.Set(_runtimeData);
                _runtimeData.IsNewGame = true;
                _sceneData.House.IsLocked = true;
            }
            else
            {
                _runtimeData = runtimeData;
                _sceneData.Npc.gameObject.SetActive(false);
                _sceneData.House.IsLocked = _runtimeData.Progress > 0;
            }


            var audioManager = Service<AudioManager>.Get();
            if(audioManager == default)
            {
                audioManager = Instantiate(_staticData.AudioManagerPrefab);
                DontDestroyOnLoad(audioManager);
                Service<AudioManager>.Set(audioManager);
            }

            var ui = Service<UI>.Get();
            if(ui == default)
            {
                ui = Instantiate(_staticData.UIPrefab);
                DontDestroyOnLoad(ui);
                Service<UI>.Set(ui);
            }

            var player = Service<Player>.Get();
            if(player == default)
            {
                player = Instantiate(_staticData.PlayerPrefab);
                DontDestroyOnLoad(player);
                Service<Player>.Set(player);
            }

            if(player.Entity.IsAlive())
            {
                player.Entity.Del<LockInputComponent>();
            }
            _sceneData.Player = player;

            if(_runtimeData.IsNewGame)
            {
                audioManager.Music.volume = audioManager.defaultVolume;
                audioManager.Wind.volume = audioManager.defaultVolume;
                ui.EndGameScreen.alpha = 0;
                ui.EndGameScreen.gameObject.SetActive(false);
                _runtimeData.IsNewGame = false;
            //    _runtimeData.IsEnd = false;
                _runtimeData.Progress = 0;
            }
        }

        private void EndGame()
        {
            _systems.Add(new FinalCutSceneSystem());
            ref var end = ref _world.NewEntity().Get<FinalCutsceneComponent>();
            _sceneData.Npc.gameObject.SetActive(false);
        }
    }
    
}