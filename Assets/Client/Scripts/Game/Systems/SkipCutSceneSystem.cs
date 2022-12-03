using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace Game
{
    public class SkipCutSceneSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RunnedCutsceneComponent, SkipCutsceneCommand> _filter = default;
        private readonly StaticData _staticData = default;
        private readonly UI _ui = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var skip = ref _filter.Get2(i);

                if(Input.GetAxis("Fire1") != 0 || Input.GetAxis("Fire2") != 0)
                {
                    skip.Duration -= Time.deltaTime;

                    if(skip.Duration <= 0)
                    {
                        ref var c = ref _filter.Get1(i);
                        c.TimeLeft = 0;
                        _filter.GetEntity(i).Del<SkipCutsceneCommand>();
                         _ui.SkipCutsceneProgress.fillAmount = 0;
                         continue;
                    }
                }
                else
                {
                    skip.Duration = _staticData.SkipCutsceneDuration;
                }

                _ui.SkipCutsceneProgress.fillAmount = 1f - skip.Duration / _staticData.SkipCutsceneDuration;
            }
        }
    }
}