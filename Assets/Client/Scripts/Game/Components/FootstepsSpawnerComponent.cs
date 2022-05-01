using UnityEngine;

namespace Game
{
    public struct FootstepsSpawnerComponent
    {
        public Transform Spawner;
        public Vector3 LastPosition;
        public float MinDistance;
        public int Steps;
        public float StepLifeTime;
        public StepsPool StepsPool;
        public Vector3[] StepOffsets;
    }
}