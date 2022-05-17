using Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class EasterDancingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EasterDancingComponent> _filter = default;
        private readonly SceneData _sceneData = default;
    
        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);

                if(cmd.Npc.Character.Agent.enabled) continue;

                if(!cmd.Npc.Character.transform.IsNear(_sceneData.Player.transform, cmd.Npc.LookRadius))
                {
                    cmd.Delay -= Time.deltaTime;
                    if(cmd.Delay <= 0)
                    {
                        cmd.Npc.Character.Animator.SetBool(AniamtionNames.Dancing, true);
                    }
                }
                else
                {
                    cmd.Npc.Character.Animator.SetBool(AniamtionNames.Dancing, false);
                    cmd.Delay = cmd.Npc.DelayForDancing;
                }
                
            }
        }
    }
}