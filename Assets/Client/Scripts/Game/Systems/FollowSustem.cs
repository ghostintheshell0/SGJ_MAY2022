using Leopotam.Ecs;

namespace Game
{
    public class FollowSystem : IEcsRunSystem
    {
        private readonly EcsFilter<FollowComponent> _filter = default;
    
        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);
                
                cmd.Follower.position = cmd.Target.position;
            }
        }
    }
}