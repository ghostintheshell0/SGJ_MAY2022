using Leopotam.Ecs;

namespace Game
{
    public class EnterNpcSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnterNpcCommand> _filter = default;
        private readonly RuntimeData _runtimeData = default;
        private readonly StaticData _staticData = default;
    
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
                    _runtimeData.Progress++;
                    cmd.Npc.AudioSource.Play();
                }
                
                if(_runtimeData.Progress < _staticData.AllCrap.Length)
                {
                    ShowBubble(cmd.Npc);
                }
                
                _filter.GetEntity(i).Del<EnterNpcCommand>();
            }
        }

        private void ShowBubble(Npc npc)
        {
            var currentCrap = _staticData.AllCrap[_runtimeData.Progress]; 
            npc.SpeechBubble.SetActive(true);
            npc.SpeechIcon.sprite = currentCrap.Icon;
            npc.SpeechIcon.color = currentCrap.Color;
            ref var showBubble = ref npc.Entity.Get<HideWithDelayComponent>();
            showBubble.Target = npc.SpeechBubble;
            showBubble.Delay = npc.ShowBubbleDuration;
        }
    }

}