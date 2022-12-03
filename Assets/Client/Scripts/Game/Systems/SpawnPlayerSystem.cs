using UnityEngine;
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
            var player = CreatePlayer();// _sceneData.Player;
            _sceneData.Player = player;
            /*
            if(!_sceneData.Player.Entity.IsAlive())
            {
                _sceneData.Player.Init(_world);
                _sceneData.Player.Footsteps.system.Clear();
            }*/
            ref var ignoreTrigger = ref player.Entity.Get<IgnoreTriggerComponent>();
            ignoreTrigger.LifeTime = _staticData.IgnoreTriggersAfterSpawnTime;
            player.Agent.Warp(spawnData.Point.position);
            player.transform.rotation = spawnData.Point.rotation;
            spawnData.OnSpawn?.Invoke(player);

            ref var playerComp = ref player.Entity.Get<PlayerComponent>();
            playerComp.View = player;

            if(_sceneData.FollowCamera)
            {
                _sceneData.CinemachineCamera.m_Follow = player.transform;
            }

            if(_sceneData.CameraFollow != default)
            {
                _sceneData.CameraFollow.Target = player.transform;
            }
            

            _sceneData.CinemachineCamera.m_LookAt = player.transform;

            if(_sceneData.Snowing != default)
            {
                ref var follow = ref _world.NewEntity().Get<FollowComponent>();
                follow.Follower = _sceneData.Snowing.transform;
                follow.Target = _sceneData.Player.transform;
            }
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

        private Character CreatePlayer()
        {
            var p = Object.Instantiate(_staticData.PlayerPrefab);
            p.Init(_world);
            if(!string.IsNullOrEmpty(_runtimeData.PickedItem))
            {
                var crapData = _staticData.GetCrapByName(_runtimeData.PickedItem);
                var itemInHand = Object.Instantiate(crapData.Prefab);
                itemInHand.Data = crapData;
                itemInHand.Init(_world);
                var picker = p.GetBehaviour<PickBehaviour>();
                ref var cmd = ref p.Entity.Get<PickNowCommand>();
                cmd.Picker = picker;
                cmd.Crap = itemInHand;
            }

            return p;
        }
    }
}