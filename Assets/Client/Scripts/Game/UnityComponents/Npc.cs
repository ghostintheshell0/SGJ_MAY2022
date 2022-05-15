using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Game
{
    public class Npc : MonoEntity
    {
        public Image SpeechIcon;
        public Collider TriggerCollider;
        public GameObject SpeechBubble;
        public Animator Animator;
        public AudioSource AudioSource;
        public FootStepsEmmiter Footsteps;
        public float ShowBubbleDuration;
        public NavMeshObstacle Obstacle;
        public NavMeshAgent Agent;
        public float LookRadius;
        public float DelayForDancing;
        public Transform Hand;

        protected override void OnInit()
        {
            var pos = SpeechBubble.transform.position;
            SpeechBubble.transform.SetParent(default, true);
            SpeechBubble.transform.position = pos;


            ref var animatedAgent = ref Entity.Get<AnimatedAgentComponent>();
            animatedAgent.Agent = Agent;
            animatedAgent.Animator = Animator;

            ref var dancing = ref Entity.Get<EasterDancingComponent>();
            dancing.Npc = this;
            dancing.Delay = DelayForDancing;

            ref var follow = ref World.NewEntity().Get<FollowComponent>();
            follow.Target = this.transform;
            follow.Follower = SpeechBubble.transform;

            if(Footsteps != default)
            {
                ref var footStepsEmmiter = ref Entity.Get<FootStepsEmmiterComponent>();
                footStepsEmmiter.View = Footsteps;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<Player>();
            if(player != default)
            {
                ref var cmd = ref player.Entity.Get<EnterNpcCommand>();
                cmd.Player = player;
                cmd.Npc = this;

                ref var lookAt = ref Entity.Get<LookAtComponent>();
                lookAt.Target = player.transform;
                lookAt.Transform = transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var player = other.GetComponent<Player>();
            if(player != default)
            {
                Entity.Del<LookAtComponent>();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, LookRadius);
        }

        public void ReadyForMove()
        {
            Obstacle.enabled = false;
            Agent.enabled = true;
            TriggerCollider.enabled = false;
            Entity.Del<LookAtComponent>();
        }
    }
}