using Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class FootStepsEmmiterSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<FootStepsEmmiterComponent> _filter = default;
        private readonly StaticData _staticData = default;
        private readonly SceneData _sceneData = default;
    
        public void Init()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);
                cmd.LastEmitPosition = cmd.View.transform.position;
                cmd.Direction = cmd.View.StartDirection;
            }
        }

        public void Run()
        {
            if(!_sceneData.EnableFootsteps) return;
            
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);
                
                if(cmd.View.Agent.enabled == false) continue;

                if (!cmd.View.transform.IsNear(cmd.LastEmitPosition, cmd.View.delta))
                {
                    var t = cmd.View.transform;
                    var pos = cmd.View.system.transform.position + (t.right * cmd.View.gap * cmd.Direction);
                    pos.y += cmd.View.YOffset;
                    cmd.Direction *= -1;
                    ParticleSystem.EmitParams ep = new ParticleSystem.EmitParams();
                    ep.position = pos;
                    ep.rotation = t.rotation.eulerAngles.y;
                    cmd.LastEmitPosition = t.position;

                    if(Physics.Raycast(pos, Vector3.down, out var hit, _staticData.PlaceOnDistance, _staticData.GroundLayers))
                    {
                        pos = hit.point;
                        pos.y += cmd.View.YOffset;
                        ep.position = pos;
                        var direction = cmd.View.Agent.velocity;
                        var r = Quaternion.LookRotation(direction.normalized, hit.normal);
                        ep.rotation3D = r.eulerAngles;
                        cmd.View.system.Emit(ep, 1);
                    }
                }
            }
        }
    }
}