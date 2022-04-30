using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class Player : MonoEntity
    {
        public NavMeshAgent Agent;
        public Transform MoveTarget;
        public Transform HandPoint;

        public float PickDistance;

        protected override void OnInit()
        {
            base.OnInit();
            ref var p = ref Entity.Get<PlayerComponent>();
            p.View = this;
        }
    }
}