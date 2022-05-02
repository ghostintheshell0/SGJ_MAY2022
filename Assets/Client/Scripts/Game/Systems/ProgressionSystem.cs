using Leopotam.Ecs;

namespace Game
{
    public class ProgressionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UpdateProgressComponent> _filter = default;
        private readonly EcsFilter<CrapComponent> _craps = default;
        private readonly StaticData _staticData = default;
        private readonly RuntimeData _runtimeData = default;
        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);
                _filter.GetEntity(i).Del<UpdateProgressComponent>();
            }

            foreach(var i in _craps)
            {
                ref var crap = ref _craps.Get1(i);
                crap.View.Collider.enabled = _staticData.AllCrap[_runtimeData.Progress].name == crap.View.Data.name;
            }
        }
    }
}