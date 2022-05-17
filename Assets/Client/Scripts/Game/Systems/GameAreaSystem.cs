using Leopotam.Ecs;
using Extensions;

namespace Game
{
    public class GameAreaSystem : IEcsRunSystem
    {
        private readonly SceneData _sceneData = default;
        private readonly UI _ui = default;
    
        public void Run()
        {
            var nearArea = GetNearArea(_sceneData.Player);
            var distance = (_sceneData.Player.transform.position - nearArea.transform.position).magnitude;

            if(distance < nearArea.SafeRadius)
            {
                return;
            }

            var endDistance = nearArea.WarningRadius;
            var warningPercent = (distance - nearArea.SafeRadius) / endDistance;
            _ui.OutGameAreaWarning.SetAlpha(warningPercent);

            if(warningPercent > 1f)
            {
                ref var respawn = ref _sceneData.Player.Entity.Get<RespawnCommand>();
                respawn.Player = _sceneData.Player;
            }
        }

        private GameArea GetNearArea(Character player)
        {
            var minDistance = float.MaxValue;
            var id = 0;

            for(var i = 0; i < _sceneData.GameAreas.Length; i++)
            {
                var sqrRadius = _sceneData.GameAreas[i].SafeRadius + _sceneData.GameAreas[i].WarningRadius;
                sqrRadius *= sqrRadius;
                var d = (player.transform.position - _sceneData.GameAreas[i].transform.position).sqrMagnitude - sqrRadius;

                if(d < minDistance)
                {
                    minDistance = d;
                    id = i;
                }
            }

            return _sceneData.GameAreas[id];
        }
    }
}