using Leopotam.Ecs;
using LeopotamGroup.Globals;
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
            var staticData = Service<StaticData>.Get();
            var cast = Physics.Raycast(pos, Vector3.down, out var hit, 10f, staticData.GroundLayers);
            if(cast)
            {
                pos = hit.point;
            }

            return pos; 
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Crap != default ? Crap.Color : Color.red;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}