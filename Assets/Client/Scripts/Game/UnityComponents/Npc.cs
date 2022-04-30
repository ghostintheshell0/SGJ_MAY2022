using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Npc : MonoEntity
    {
        public Image SpeechIcon;
        public GameObject SpeechBubble;
        
        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<Player>();
            if(player != default)
            {
                ref var cmd = ref player.Entity.Get<EnterNpcCommand>();
                cmd.Player = player;
                cmd.Npc = this;
            }
        }
    }
}