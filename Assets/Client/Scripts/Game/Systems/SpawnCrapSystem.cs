using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class SpawnCrapSystem : IEcsInitSystem
    {
        private readonly EcsFilter<CrapSpawnerComponent> _filter = default;
        private readonly EcsFilter<RespawnCrapCommand> _respawns = default;
        private readonly EcsFilter<CrapComponent> _craps = default;
        private readonly StaticData _staticData = default;
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
                crap.Data = cmd.View.Crap;
                ref var crapComponent = ref crap.Entity.Get<CrapComponent>();
                crapComponent.Spawner = cmd.View;
            }

            if(!_respawns.IsEmpty())
            {
                foreach(var i in _respawns)
                {
                    _respawns.GetEntity(i).Del<RespawnCrapCommand>();
                }

                if(_staticData.RespawnCrap)
                {
                    foreach(var i in _craps)
                    {
                        ref var crap = ref _craps.Get1(i);
                        crap.View.transform.position = crap.Spawner.GetSpawnPoint();
                    }
                }
            }

            _world.NewEntity().Get<UpdateProgressComponent>();
        }
    }
}