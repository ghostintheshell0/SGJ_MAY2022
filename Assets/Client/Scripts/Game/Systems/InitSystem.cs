using Leopotam.Ecs;

namespace Game
{
    public class InitSystem : IEcsRunSystem
    {
        private readonly SceneData _sceneData = default;
        private readonly EcsWorld _world = default;
    
        public void Run()
        {
            foreach(var monoEntity in _sceneData.Entities)
            {
                monoEntity.Init(_world);
            }
        }
    }
}