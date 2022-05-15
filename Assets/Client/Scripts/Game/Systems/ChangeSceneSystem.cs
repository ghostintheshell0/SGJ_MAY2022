using Leopotam.Ecs;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Game
{
    public class ChangeSceneSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ChangeSceneComponent> _filter = default;
        private readonly RuntimeData _runtimeData = default;
        private readonly UI _ui = default;
    
        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);
                
                cmd.Player.Entity.Get<LockInputComponent>();
                cmd.Player.Agent.SetDestination(cmd.MovePoint.position);
                var t = _ui.ChangeSceneFade.DOFade(1f, _ui.ChangeSceneFadeDuration);
                var targetScene = cmd.NextSceneName;
                t.onComplete += () => ChangeScene(targetScene);
                _runtimeData.PreviousScene = SceneManager.GetActiveScene().name;
                _filter.GetEntity(i).Del<ChangeSceneComponent>();
            }
        }

        private void ChangeScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }
    }
}