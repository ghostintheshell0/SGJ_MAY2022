using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class NpcBehaviour : CharacterBehaviour
    {
        public Collider TriggerCollider;
        public NavMeshObstacle Obstacle;

        protected override void OnInit()
        {
            ref var npc = ref Character.Entity.Get<NpcComponent>();
            npc.View = this;
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<Character>();
            if(player != default && player.Entity.Has<PlayerComponent>())
            {
                ref var cmd = ref player.Entity.Get<EnterNpcCommand>();
                cmd.Player = player;
                cmd.Npc = this;

                ref var lookAt = ref Character.Entity.Get<LookAtComponent>();
                lookAt.Target = player.transform;
                lookAt.Transform = transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var player = other.GetComponent<Character>();
            if(player != default)
            {
                Character.Entity.Del<LookAtComponent>();
            }
        }

        public void ReadyForMove()
        {
            Obstacle.enabled = false;
            Character.Agent.enabled = true;
            TriggerCollider.enabled = false;
            Character.Entity.Del<LookAtComponent>();
        }
    }
}