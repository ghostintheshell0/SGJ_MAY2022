using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class Crap : MonoEntity
    {
        public Collider Collider;
        public NavMeshObstacle Obstacle;

        protected override void OnInit()
        {
            base.OnInit();
            ref var c = ref Entity.Get<CrapComponent>();
            c.View = this;
        }
    }
}