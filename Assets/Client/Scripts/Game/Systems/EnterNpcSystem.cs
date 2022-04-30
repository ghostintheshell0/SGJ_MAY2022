using Leopotam.Ecs;

namespace Game
{
    public class EnterNpcSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnterNpcCommand> _filter = default;
    
        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);

                ref var player = ref cmd.Player.Entity.Get<PlayerComponent>();
                if(player.CurrentCrap != default)
                {
                    UnityEngine.Object.Destroy(player.CurrentCrap.gameObject);
                    player.CurrentCrap = default;
                    cmd.Npc.SpeechBubble.SetActive(false);
                }
                _filter.GetEntity(i).Del<EnterNpcCommand>();
            }
        }
    }
}