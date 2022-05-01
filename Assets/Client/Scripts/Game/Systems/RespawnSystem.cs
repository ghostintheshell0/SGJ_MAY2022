using DG.Tweening;
using Leopotam.Ecs;

namespace Game
{
    public class RespawnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RespawnCommand> _filter = default;
        private readonly SceneData _sceneData = default;
    
        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);
                
                _sceneData.OutGameAreaWarning.DOFade(0, 1).SetDelay(cmd.Player.RespawnDuration);
                cmd.Player.Agent.Warp(cmd.Player.SpawnPoint.position);
                cmd.Player.MoveTarget.position = cmd.Player.SpawnPoint.position;

                ref var player = ref cmd.Player.Entity.Get<PlayerComponent>();
                cmd.Player.AudioSource.PlayOneShot(_sceneData.RespawnClip);

                SendCrapBack(player.CurrentCrap);
                player.CurrentCrap = default;

                _filter.GetEntity(i).Del<RespawnCommand>();
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