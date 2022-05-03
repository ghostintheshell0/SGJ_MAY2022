using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class House : MonoEntity
    {
        public Collider Trigger;
        public Transform HousePoint;
        public Transform ExitPoint;
        public string SceneName;
        public bool IsLocked;

        protected override void OnInit()
        {
            ref var spawner = ref Entity.Get<PlayerSpawnerComponent>();
            spawner.Name = SceneName;
            spawner.Point = ExitPoint;
            spawner.OnSpawn += Spawn;
        }
        
        private void OnTriggerEnter(Collider collider)
        {
            if(IsLocked) return;
            var player = collider.GetComponent<Player>();

            if(player != default)
            {
                if(player.Entity.Has<IgnoreTriggerComponent>()) return;
                ref var cmd = ref Entity.Get<ChangeSceneComponent>();
                cmd.MovePoint = HousePoint;
                cmd.Player = player;
                cmd.SceneName = SceneName;
            }
        }

        private void Spawn(Player player)
        {
        }

    }
}