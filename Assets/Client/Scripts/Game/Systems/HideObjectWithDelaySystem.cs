using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class HideObjectWithDelaySystem : IEcsRunSystem
    {
        private readonly EcsFilter<HideWithDelayComponent> _filter = default;
    
        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);
                
                cmd.Delay -= Time.deltaTime;

                if(cmd.Delay <= 0)
                {
                    cmd.Target.SetActive(false);
                    _filter.GetEntity(i).Del<HideWithDelayComponent>();
                }
            }
        }
    }
}