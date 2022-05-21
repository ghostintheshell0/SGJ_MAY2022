using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class ChangeSceneTrigger : MonoEntity
    {
        public Transform EnterPoint;
        public Transform ExitPoint;
        public string SceneName;
        public bool SpawnEnabled;

        protected override void OnInit()
        {
            if(SpawnEnabled)
            {
                ref var spawner = ref Entity.Get<PlayerSpawnerComponent>();
                spawner.Name = SceneName;
                spawner.Point = ExitPoint;
            }
        }


        private void OnTriggerEnter(Collider collider)
        {
            var player = collider.GetComponent<Character>();

            if(player != default && player.Entity.Has<PlayerComponent>())
            {
                if(player.Entity.Has<IgnoreTriggerComponent>()) return;
                ref var cmd = ref Entity.Get<ChangeSceneComponent>();
                cmd.MovePoint = EnterPoint;
                cmd.Player = player;
                cmd.NextSceneName = SceneName;
            }
        }
    }
}