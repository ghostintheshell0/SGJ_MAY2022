using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class DancingWhenAloneBehaviour : CharacterBehaviour
    {
        public float DelayForDancing;
        public float LookRadius;

        protected override void OnInit()
        {
            ref var dancing = ref Character.Entity.Get<EasterDancingComponent>();
            dancing.Npc = this;
            dancing.Delay = DelayForDancing;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, LookRadius);
        }

    }
}