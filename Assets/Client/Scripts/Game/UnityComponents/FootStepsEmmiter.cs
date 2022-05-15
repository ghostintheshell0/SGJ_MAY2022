using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class FootStepsEmmiter : MonoBehaviour
    {
        public ParticleSystem system;
        public NavMeshAgent Agent;

        public float delta = 1;
        public float gap = 0.5f;
        public float YOffset;
        public int StartDirection = 1;
    }
}