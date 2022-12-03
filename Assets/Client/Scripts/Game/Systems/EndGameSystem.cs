using Leopotam.Ecs;

namespace Game
{
    public class EndGameSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayCutsceneComponent> _filter = default;
        private readonly RuntimeData _runtimeData = default;
        private readonly EcsWorld _world = default;
        private readonly SceneData _sceneData = default;
    
        public void Run()
        {
            if(_runtimeData.IsEnd && _filter.IsEmpty())
            {
                ref var c = ref _world.NewEntity().Get<PlayCutsceneComponent>();
                
                _sceneData.Snowing.gameObject.SetActive(false);

                if(_sceneData.Player != default)
                {
                    _sceneData.Player.gameObject.SetActive(false);
                }
            }
        }
    }
}