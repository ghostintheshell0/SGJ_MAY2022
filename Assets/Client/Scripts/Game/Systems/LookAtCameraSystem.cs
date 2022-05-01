using Leopotam.Ecs;

namespace Game
{
    public class LookAtCameraSystem : IEcsRunSystem
    {
        private readonly EcsFilter<LookAtCameraComponent> _filter = default;
    
        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);
                cmd.Transform.LookAt(cmd.Camera.transform);
            }
        }
    }
}