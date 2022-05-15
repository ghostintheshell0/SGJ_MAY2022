using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class FixCrapSpawnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<FixCrapSpawnComponent> _filter = default;
    
        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);
                
                cmd.Delay -= Time.deltaTime;

                if(cmd.Delay <= 0)
                {
                    cmd.View.Agent.enabled = false;
                    cmd.View.Obstacle.enabled = true;
                    _filter.GetEntity(i).Del<FixCrapSpawnComponent>();
                }
                else
                {
                    cmd.View.Obstacle.enabled = false;
                    cmd.View.Agent.enabled = true;
                }
            }
        }
    }
}