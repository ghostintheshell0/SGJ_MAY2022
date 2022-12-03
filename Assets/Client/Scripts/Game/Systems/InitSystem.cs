using DG.Tweening;
using Extensions;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace Game
{
    public class InitSystem : IEcsInitSystem
    {
        private readonly SceneData _sceneData = default;
        private readonly EcsWorld _world = default;
        private readonly UI _ui = default;

        private string volumeValueName = "MasterVolume";
    
        public void Init()
        {
            foreach(var monoEntity in _sceneData.Entities)
            {
                monoEntity.Init(_world);
            }
           
            
            var audioManager = Service<AudioManager>.Get();
            if(audioManager.AudioGroup.audioMixer.GetFloat(volumeValueName, out var volume))
            {
                _ui.AudioSlider.value = volume;
                _ui.AudioSlider.onValueChanged.AddListener(ChangeAudio);
            }
            else
            {
                Debug.LogWarning($"AudioMixer param {volumeValueName} not found");
            }


            _ui.ChangeSceneFade.SetAlpha(1);
            _ui.ChangeSceneFade.DOFade(0f, _ui.ChangeSceneFadeDuration);

            _world.NewEntity().Get<UpdateProgressEvent>();

            var windVolume = _sceneData.EnableWind ? 1 : 0;

            Service<AudioManager>.Get().Wind.DOFade(windVolume, 1f);
        }

        private void ChangeAudio(float value)
        {
            var audioManager = Service<AudioManager>.Get();
            audioManager.AudioGroup.audioMixer.SetFloat(volumeValueName, value);
        }
    }
}