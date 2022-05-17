using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class SpeakBehaviour : CharacterBehaviour
    {
        public Image SpeechIcon;
        public Collider TriggerCollider;
        public GameObject SpeechBubble;
        public float ShowBubbleDuration;


        protected override void OnInit()
        {
            var pos = SpeechBubble.transform.position;
            SpeechBubble.transform.SetParent(default, true);
            SpeechBubble.transform.position = pos;

            var speechBubbleEnt = Character.World.NewEntity();
            ref var follow = ref speechBubbleEnt.Get<FollowComponent>();
            follow.Target = this.transform;
            follow.Follower = SpeechBubble.transform;

            ref var lookAt = ref speechBubbleEnt.Get<LookAtComponent>();
            lookAt.Target = Service<SceneData>.Get().MainCamera.transform;
            lookAt.Transform = SpeechBubble.transform;

            ref var speeker = ref Character.Entity.Get<SpeekerComponent>();
            speeker.View = this;
        }
    }
}