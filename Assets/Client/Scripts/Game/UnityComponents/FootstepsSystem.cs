using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class FootstepsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<FootstepsSpawnerComponent> _filter = default;
        private readonly EcsFilter<FootstepsComponent> _steps = default;
        private readonly EcsWorld _world = default;
    
        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);
                
                var d = (cmd.Spawner.position - cmd.LastPosition).sqrMagnitude;

                if(d > cmd.MinDistance * cmd.MinDistance)
                {
                    cmd.LastPosition = cmd.Spawner.position;
                    var stepView = cmd.StepsPool.Get();
                    ref var stepComp = ref _world.NewEntity().Get<FootstepsComponent>();
                    var pos = cmd.Spawner.position + cmd.StepOffsets[cmd.Steps % cmd.StepOffsets.Length];
                    stepView.transform.position = pos;
                    stepView.transform.rotation = cmd.Spawner.rotation;
                    stepComp.View = stepView;
                    stepComp.MaxLifeTime = cmd.StepLifeTime;
                    stepComp.LifeTime = cmd.StepLifeTime;
                    stepComp.SpawnerEnt = _filter.GetEntity(i);
                    cmd.Steps++;
                }
            }

            foreach(var i in _steps)
            {
                ref var step = ref _steps.Get1(i);

                step.LifeTime -= Time.deltaTime;

                if(step.LifeTime <= 0)
                {
                    ref var spawner = ref step.SpawnerEnt.Get<FootstepsSpawnerComponent>();
                    spawner.StepsPool.Recycle(step.View);
                    _steps.GetEntity(i).Del<FootstepsComponent>();
                    continue;
                }

                SetAlpha(step);
            }
        }

        private void SetAlpha(in FootstepsComponent step)
        {
            var a = step.LifeTime / step.MaxLifeTime;
            step.View.Projector.fadeFactor = a;
        }
    }
}