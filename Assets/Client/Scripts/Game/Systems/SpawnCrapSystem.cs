using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class SpawnCrapSystem : IEcsInitSystem
    {
        private readonly EcsFilter<CrapSpawnerComponent> _filter = default;
        private readonly EcsWorld _world = default;
    
        public void Init()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);
                var prefab = cmd.View.Crap.Prefab;
                var pos = cmd.View.GetSpawnPoint(); 
                var crap = UnityEngine.Object.Instantiate(prefab, pos, Quaternion.identity);
                crap.Init(_world);
                ref var crapComponent = ref crap.Entity.Get<CrapComponent>();
                crapComponent.Spawner = cmd.View;
            }
        }
    }
}