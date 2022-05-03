using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class RespawnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RespawnCommand> _filter = default;
        private readonly SceneData _sceneData = default;
        private readonly UI _ui = default;
    
        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);
                
                if(cmd.Inited)
                {
                    cmd.Delay -= Time.deltaTime;
                
                    if(cmd.Delay < 0)
                    {
                        cmd.Player.Entity.Del<LockInputComponent>();
                        _filter.GetEntity(i).Del<RespawnCommand>();
                    }
                }
                else
                {
                    _ui.OutGameAreaWarning.DOFade(0, 1).SetDelay(cmd.Player.RespawnDuration);
                    var spawnPos = _sceneData.SpawnPoint.position;
                    cmd.Player.Agent.Warp(spawnPos);
                    cmd.Player.MoveTarget.position = spawnPos;
                    cmd.Player.Agent.SetDestination(spawnPos);

                    ref var player = ref cmd.Player.Entity.Get<PlayerComponent>();
                    cmd.Player.AudioSource.PlayOneShot(_sceneData.RespawnClip);

                    SendCrapBack(player.CurrentCrap);
                    player.CurrentCrap = default;
                    cmd.Delay = cmd.Player.RespawnDuration;
                    cmd.Player.Entity.Get<LockInputComponent>();
                    cmd.Inited = true;
                }
               

            }
        }

        private void SendCrapBack(Crap crap)
        {
            if(crap != default)
            {
                crap.transform.SetParent(default);
                crap.Collider.enabled = true;
                crap.Obstacle.enabled = true;
                ref var crapComp = ref crap.Entity.Get<CrapComponent>();
                crap.transform.position = crapComp.Spawner.GetSpawnPoint();
            }
        }
    }
}