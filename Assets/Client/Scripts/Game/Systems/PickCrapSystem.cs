using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class PickCrapSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PickCommand> _filter = default;
    
        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);
                ref var player = ref cmd.Picker.Entity.Get<PlayerComponent>();
                player.CurrentCrap = cmd.Crap;
                cmd.Crap.Collider.enabled = false;
                cmd.Crap.transform.position = player.View.HandPoint.position;
                cmd.Crap.transform.SetParent(player.View.HandPoint);
                _filter.GetEntity(i).Del<PickCommand>();
            }
        }
    }
}