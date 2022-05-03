using Leopotam.Ecs;

namespace Game
{
    public class SpawnPlayerSystem : IEcsInitSystem
    {
        private readonly StaticData _staticData = default;
        private readonly SceneData _sceneData = default;
        private readonly RuntimeData _runtimeData = default;
        private readonly EcsWorld _world = default;

        private readonly EcsFilter<PlayerSpawnerComponent> _spawners = default;
    
        public void Init()
        {
            var spawnData = GetSpawner(_runtimeData.PreviousScene);
            var player = _sceneData.Player;
            if(!_sceneData.Player.Entity.IsAlive())
            {
                _sceneData.Player.Init(_world);
            }
            ref var ignoreTrigger = ref player.Entity.Get<IgnoreTriggerComponent>();
            ignoreTrigger.LifeTime = _staticData.IgnoreTriggersAfterSpawnTime;
            player.Agent.Warp(spawnData.Point.position);
            player.transform.rotation = spawnData.Point.rotation;
            spawnData.OnSpawn?.Invoke(player);

            if(_sceneData.FollowCamera)
            {
                _sceneData.CinemachineCamera.m_Follow = player.transform;
            }

            _sceneData.CinemachineCamera.m_LookAt = player.transform;
        }

        private PlayerSpawnerComponent GetSpawner(string name)
        {
            foreach(var i in _spawners)
            {
                ref var spawner = ref _spawners.Get1(i);
                if(spawner.Name == name)
                {
                    return spawner;
                }
            }

            var spawnData = new PlayerSpawnerComponent();
            spawnData.Point = _sceneData.SpawnPoint;
            return spawnData;
        }
    }
}