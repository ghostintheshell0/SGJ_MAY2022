using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class SpawnCrapSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<CrapSpawnerComponent> _filter = default;
        private readonly EcsFilter<RespawnCrapCommand> _respawns = default;
        private readonly EcsFilter<CrapComponent> _craps = default;
        private readonly StaticData _staticData = default;
        private readonly SceneData _sceneData = default;
        private readonly EcsWorld _world = default;
        private readonly RuntimeData _runtimeData = default;
    
        public void Init()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);
                var index = _staticData.AllCrap.IndexOf(cmd.View.Crap);
                if(index < _runtimeData.Progress) continue;

                if(_sceneData.Player.CurrentCrap != default && _sceneData.Player.CurrentCrap.Data.name == cmd.View.Crap.name) continue;
                var prefab = cmd.View.Crap.Prefab;
                var pos = cmd.View.GetSpawnPoint(); 
                var crap = UnityEngine.Object.Instantiate(prefab, pos, Quaternion.identity);
                crap.Init(_world);
                crap.Data = cmd.View.Crap;
                ref var crapComponent = ref crap.Entity.Get<CrapComponent>();
                crapComponent.Spawner = cmd.View;
            }

            _world.NewEntity().Get<UpdateProgressComponent>();
        }

        public void Run()
        {
            if(!_respawns.IsEmpty())
            {
                foreach(var i in _respawns)
                {
                    _respawns.GetEntity(i).Del<RespawnCrapCommand>();
                }

                foreach(var i in _craps)
                {
                    ref var crap = ref _craps.Get1(i);
                    if(_staticData.RespawnCrap)
                    {
                        if(_sceneData.Player.CurrentCrap != default && _sceneData.Player.CurrentCrap.Data.name == crap.View.Data.name) continue;
                        crap.View.transform.position = crap.Spawner.GetSpawnPoint();
                    }
                }
            }
        }
    }
}