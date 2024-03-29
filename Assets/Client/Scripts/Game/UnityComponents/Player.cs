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
        public Animator Animator;
        public Transform SpawnPoint;

        public float PickDistance;
        public float PrePickDuration;
        public float PostPickDuration;

        public float RespawnDuration;
        public AudioSource AudioSource;


        protected override void OnInit()
        {
            base.OnInit();
            ref var p = ref Entity.Get<PlayerComponent>();
            p.View = this;

            ref var animatedAgent = ref Entity.Get<AnimatedAgentComponent>();
            animatedAgent.Agent = Agent;
            animatedAgent.Animator = Animator;
        }
    }
}