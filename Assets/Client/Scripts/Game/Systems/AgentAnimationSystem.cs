using Leopotam.Ecs;

namespace Game
{
    public class AgentAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AnimatedAgentComponent> _filter = default;
    
        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);
                
                var isMove = cmd.Agent.velocity.sqrMagnitude > 0.1f;

                cmd.Animator.SetBool(AniamtionNames.Move, isMove);

            }
        }
    }
}