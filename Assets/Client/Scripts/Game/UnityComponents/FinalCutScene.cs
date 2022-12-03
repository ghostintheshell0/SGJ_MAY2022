using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

namespace Game
{
    public class FinalCutScene : MonoEntity
    {
        public KeyCode DebugKey;
        public PlayableDirector Timeline;
        
        public CinemachineVirtualCamera CutSceneCamera;
        public CinemachineVirtualCamera CutSceneCamera2;
        public CinemachineVirtualCamera CutSceneCamera3;
        public CinemachineVirtualCamera CutSceneCamera4;
        
        public Animator HouseAnimator;

        public float SunRotationDuration;
        public Vector3 SunRotation;
        public Transform Sun;

        public Transform Traktor;
        public float HeightDuration;
        public float TraktorHeight;
        public float TraktorSpeed;
        public Rigidbody TraktorBody;

        public float ZeroPartDuration;
        public float FirstPartDuration;
        public float SecondPartDuration;
        public float ThirtdPartDuration;
        public float FourthPartDuration;
        public float FifthPartDuration;

        public float Camera1BlendDuration;
        public float Camera2BlendDuration;

        public float TraktorFlyAngle;
        public float TraktorFlyAngleDuration;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            var height = Traktor.position;
            height.y += TraktorHeight;
            Gizmos.DrawLine(Traktor.position, height);
        }
    }
}