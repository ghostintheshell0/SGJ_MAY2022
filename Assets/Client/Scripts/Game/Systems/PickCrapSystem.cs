using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class PickCrapSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PickCommand> _commands = default;
        private readonly EcsFilter<PickComponent> _filter = default;
        private readonly EcsFilter<PickNowCommand> _pickNowFilter = default;
    
        public void Run()
        {
            foreach(var i in _commands)
            {
                ref var cmd = ref _commands.Get1(i);
                ref var player = ref cmd.Picker.Character.Entity.Get<PlayerComponent>();
                if(!player.View.Entity.Has<PickComponent>() || player.View.CurrentCrap != default)
                {
                    ref var comp = ref cmd.Picker.Character.Entity.Get<PickComponent>();
                    comp.Picker = cmd.Picker;
                    comp.Target = cmd.Crap;
                    comp.PrePickDuration = cmd.Picker.PrePickDuration;
                    comp.PostPickDuration = cmd.Picker.PrePickDuration + cmd.Picker.PostPickDuration;
                    cmd.Picker.Character.Animator.SetBool(AniamtionNames.Pick, true);
                    comp.Picker.transform.forward = (cmd.Crap.transform.position - cmd.Picker.transform.position).normalized;
                    cmd.Picker.Character.Agent.enabled = false;
                    comp.Picker.Character.Entity.Get<LockInputComponent>();
                }
                
                _commands.GetEntity(i).Del<PickCommand>();
            }

            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);

                cmd.PrePickDuration -= Time.deltaTime;
                cmd.PostPickDuration -= Time.deltaTime;

                if(!cmd.Picked  && cmd.PrePickDuration <= 0)
                {
                    ref var pickNow = ref cmd.Picker.Character.Entity.Get<PickNowCommand>();
                    pickNow.Picker = cmd.Picker;
                    pickNow.Crap = cmd.Target;
                   
                    cmd.Picked = true;
                }

                if(cmd.PostPickDuration <= 0)
                {
                    cmd.Picker.Character.Animator.SetBool(AniamtionNames.Pick, false);
                    cmd.Picker.Character.Agent.enabled = true;
                    cmd.Picker.Character.Entity.Del<LockInputComponent>();
                    _filter.GetEntity(i).Del<PickComponent>();
                }
            }

            foreach(var i in _pickNowFilter)
            {
                ref var cmd = ref _pickNowFilter.Get1(i);
                ref var player = ref cmd.Picker.Character.Entity.Get<PlayerComponent>();
                player.View.CurrentCrap = cmd.Crap;
                cmd.Crap.Collider.enabled = false;
                cmd.Crap.Obstacle.enabled = false;
                cmd.Crap.transform.SetParent(player.View.HandPoint);
                cmd.Crap.transform.position = player.View.HandPoint.position;
                player.View.AudioSource.PlayOneShot(cmd.Crap.PickClip);
                player.View.CurrentCrap = cmd.Crap;
                cmd.Crap.Entity.Del<FixCrapSpawnComponent>();
                _pickNowFilter.GetEntity(i).Del<PickNowCommand>();
            }
        }
    }

    public struct PickNowCommand
    {
        public PickBehaviour Picker;
        public Crap Crap;

    }
}