using Leopotam.Ecs;

namespace Game
{
    public class ColliderUnlockSysytem : IEcsRunSystem
    {
        private readonly EcsFilter<ColliderUnlockerComponent> _filter = default;
        private readonly EcsFilter<UpdateProgressEvent> _events = default;
        private readonly RuntimeData _runtimeData = default;
    
        public void Run()
        {
            if(!_events.IsEmpty())
            {
                foreach(var i in _filter)
                {
                    ref var cmd = ref _filter.Get1(i);
                    var isEnabled = cmd.View.Progress.IsCompleted();

                    foreach(var collider in cmd.View.Colliders)
                    {
                        collider.enabled = isEnabled;
                    }
                    
                }
            }
        }
    }
}