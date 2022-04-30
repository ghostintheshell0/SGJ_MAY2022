using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class Crap : MonoEntity
    {
        public Collider Collider;

        protected override void OnInit()
        {
            base.OnInit();
            ref var c = ref Entity.Get<CrapComponent>();
            c.View = this;
        }
    }
}