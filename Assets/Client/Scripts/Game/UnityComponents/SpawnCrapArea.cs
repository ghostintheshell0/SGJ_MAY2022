using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class SpawnCrapArea : MonoEntity
    {
        public CrapData Crap;
        public float Radius;

        protected override void OnInit()
        {
            base.OnInit();
            ref var spawn = ref Entity.Get<CrapSpawnerComponent>();
            spawn.View = this;
        }

        public Vector3 GetSpawnPoint()
        {
            var random = Random.insideUnitCircle;
            var pos = new Vector3(random.x, 0f, random.y) * Radius + transform.position;
            return pos; 
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Crap != default ? Crap.Color : Color.red;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}