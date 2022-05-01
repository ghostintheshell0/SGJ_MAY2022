using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class PickCrapSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PickCommand> _commands = default;
        private readonly EcsFilter<PickComponent> _filter = default;
    
        public void Run()
        {
            foreach(var i in _commands)
            {
                ref var cmd = ref _commands.Get1(i);
                ref var player = ref cmd.Picker.Entity.Get<PlayerComponent>();
                if(!player.View.Entity.Has<PickComponent>())
                {
                    ref var comp = ref cmd.Picker.Entity.Get<PickComponent>();
                    comp.Player = cmd.Picker;
                    comp.Target = cmd.Crap;
                    comp.PrePickDuration = cmd.Picker.PrePickDuration;
                    comp.PostPickDuration = cmd.Picker.PrePickDuration + cmd.Picker.PostPickDuration;
                    cmd.Picker.Animator.SetBool(AniamtionNames.Pick, true);
                    cmd.Picker.Agent.enabled = false;
                }
                
                _commands.GetEntity(i).Del<PickCommand>();
            }

            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);

                cmd.PrePickDuration -= Time.deltaTime;
                cmd.PostPickDuration -= Time.deltaTime;

                if(cmd.PrePickDuration <= 0)
                {
                    ref var player = ref cmd.Player.Entity.Get<PlayerComponent>();
                    player.CurrentCrap = cmd.Target;
                    cmd.Target.Collider.enabled = false;
                    cmd.Target.Obstacle.enabled = false;
                    cmd.Target.transform.position = player.View.HandPoint.position;
                    cmd.Target.transform.SetParent(player.View.HandPoint);
                }

                if(cmd.PostPickDuration <= 0)
                {
                    cmd.Player.Animator.SetBool(AniamtionNames.Pick, false);
                    cmd.Player.Agent.enabled = true;
                    _filter.GetEntity(i).Del<PickComponent>();
                }
            }
        }
    }
}