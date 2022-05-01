using UnityEngine;

namespace Game
{
    public class GameArea : MonoEntity
    {
        public float SafeRadius;
        public float WarningRadius;

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, SafeRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, SafeRadius + WarningRadius);
        }
    }

}