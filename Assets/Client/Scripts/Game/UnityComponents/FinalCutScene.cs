using UnityEngine;
using Cinemachine;

namespace Game
{
    public class FinalCutScene : MonoEntity
    {
        public KeyCode DebugKey;
        public House House;
        public Player Player;
        public CinemachineVirtualCamera CutSceneCamera;
        public CinemachineVirtualCamera CutSceneCamera2;
        public Npc Npc;
        public Transform HousePoint;
        public Animator HouseAnimator;

        public float SunRotationDuration;
        public Vector3 SunRotation;
        public Transform Sun;

        public Transform Traktor;
        public float HeightDuration;
        public float TraktorHeight;
        public float TraktorSpeed;
        public Rigidbody TraktorBody;

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