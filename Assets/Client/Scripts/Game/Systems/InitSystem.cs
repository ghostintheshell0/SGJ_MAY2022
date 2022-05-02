using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class InitSystem : IEcsInitSystem
    {
        private readonly SceneData _sceneData = default;
        private readonly EcsWorld _world = default;

        private string volumeValueName = "MasterVolume";
    
        public void Init()
        {
            foreach(var monoEntity in _sceneData.Entities)
            {
                monoEntity.Init(_world);
            }
            
            ref var lookAt = ref _world.NewEntity().Get<LookAtComponent>();
            lookAt.Target = _sceneData.MainCamera.transform;
            lookAt.Transform = _sceneData.Npc.SpeechBubble.transform;
           
            
            if(_sceneData.AudioGroup.audioMixer.GetFloat(volumeValueName, out var volume))
            {
                _sceneData.AudioSlider.value = volume;
                _sceneData.AudioSlider.onValueChanged.AddListener(ChangeAudio);
            }
            else
            {
                Debug.LogWarning($"AudioMixer param {volumeValueName} not found");
            }

        }

        private void ChangeAudio(float value)
        {
            _sceneData.AudioGroup.audioMixer.SetFloat(volumeValueName, value);
        }
    }
}