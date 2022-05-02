using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class FootStepsEmmiter : MonoBehaviour
    {
        public ParticleSystem system;
        public NavMeshAgent agent;

        Vector3 lastEmit;

        public float delta = 1;
        public float gap = 0.5f;
        public float YOffset;
        int dir = 1;
        static FootStepsEmmiter selectedSystem;
        public void Update()
        {

            if (Vector3.Distance(lastEmit, transform.position) > delta)
            {
                Gizmos.color = Color.green;
                var pos = transform.position + (transform.right * gap * dir);
                pos.y += YOffset;
                dir *= -1;
                ParticleSystem.EmitParams ep = new ParticleSystem.EmitParams();
                ep.position = pos;
                ep.rotation = transform.rotation.eulerAngles.y;
                system.Emit(ep, 1);
                lastEmit = transform.position;
            }

        }
    }
}