using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class Character : MonoEntity
    {
        public NavMeshAgent Agent;
        public Transform HandPoint;
        public Animator Animator;
        public FootStepsEmmiter Footsteps;
        public AudioSource AudioSource;

        public Crap CurrentCrap {get; set;}

        public CharacterBehaviour[] Behaviours;

        protected override void OnInit()
        {
            base.OnInit();

            foreach(var behaviour in Behaviours)
            {
                behaviour.Init(this);
            }

            if(Footsteps != default)
            {
                ref var footStepsEmmiter = ref Entity.Get<FootStepsEmmiterComponent>();
                footStepsEmmiter.View = Footsteps;
            }
            
            ref var animatedAgent = ref Entity.Get<AnimatedAgentComponent>();
            animatedAgent.Agent = Agent;
            animatedAgent.Animator = Animator;
        }

        public T GetBehaviour<T>() where T : CharacterBehaviour
        {
            foreach(var behaviour in Behaviours)
            {
                if(behaviour.GetType() == typeof(T))
                {
                    return behaviour as T;
                }
            }

            return default;
        }
    }
}