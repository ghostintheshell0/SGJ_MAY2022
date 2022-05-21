using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class ColliderUnlocker : MonoEntity
    {
        public int Progress;
        public Collider[] Colliders;

        protected override void OnInit()
        {
            base.OnInit();
            ref var c = ref Entity.Get<ColliderUnlockerComponent>();
            c.View = this;
        }
    }
}