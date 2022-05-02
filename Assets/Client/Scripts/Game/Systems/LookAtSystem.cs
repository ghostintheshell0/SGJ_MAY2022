using Leopotam.Ecs;

namespace Game
{
    public class LookAtSystem : IEcsRunSystem
    {
        private readonly EcsFilter<LookAtComponent> _filter = default;
    
        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);
                cmd.Transform.LookAt(cmd.Target.transform, UnityEngine.Vector3.up);
            }
        }
    }
}