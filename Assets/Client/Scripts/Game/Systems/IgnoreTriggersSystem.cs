using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class IgnoreTriggersSystem : IEcsRunSystem
    {
        private readonly EcsFilter<IgnoreTriggerComponent> _filter = default;
    
        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);

                cmd.LifeTime -= Time.deltaTime;

                if(cmd.LifeTime <= 0)
                {
                    _filter.GetEntity(i).Del<IgnoreTriggerComponent>();
                }
            }
        }
    }
}