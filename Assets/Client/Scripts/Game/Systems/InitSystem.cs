using Leopotam.Ecs;

namespace Game
{
    public class InitSystem : IEcsInitSystem
    {
        private readonly SceneData _sceneData = default;
        private readonly EcsWorld _world = default;
    
        public void Init()
        {
            foreach(var monoEntity in _sceneData.Entities)
            {
                monoEntity.Init(_world);
            }
            
            ref var lookAt = ref _sceneData.Npc.Entity.Get<LookAtCameraComponent>();
            lookAt.Camera = _sceneData.MainCamera;
            lookAt.Transform = _sceneData.Npc.SpeechBubble.transform;
        }
    }
}