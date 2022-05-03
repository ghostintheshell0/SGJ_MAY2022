using Leopotam.Ecs;
using NaughtyAttributes;
using UnityEngine;

namespace Game
{
    public class House : MonoEntity
    {

        public Transform DoorObstcale;
        public Transform HousePoint;
        public Transform ExitPoint;
        public string SceneName;
        [SerializeField] private bool isLocked;

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

        [Button]
        public void ChangeLock()
        {
            IsLocked = !IsLocked;
        }

        public bool IsLocked
        {
            get => isLocked;
            set
            {
                isLocked = value;
                if(DoorObstcale != default)
                {
                    DoorObstcale.gameObject.SetActive(value);
                }
                
            }
        }
    }
}