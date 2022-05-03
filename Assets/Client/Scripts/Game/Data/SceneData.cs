using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Cinemachine;
using LeopotamGroup.Globals;

namespace Game
{
    public class SceneData : MonoBehaviour
    {
        public Camera MainCamera;
        public CinemachineBrain CinemachineBrain;
        public CinemachineVirtualCamera CinemachineCamera;
        public Player Player;
        public House House;
        public Transform SpawnPoint;
        public Npc Npc;

        public GameArea[] GameAreas;

        public MonoEntity[] Entities;
        public AudioClip RespawnClip;
        public FinalCutScene FinalCutScene;
    
        [Button]
        public void Grab()
        {
            Entities = GameObject.FindObjectsOfType<MonoEntity>();
        }

        public void Restart()
        {
            var runtimeData = Service<RuntimeData>.Get();
            runtimeData.IsNewGame = true;
            SceneManager.LoadScene(gameObject.scene.name);
        }
    }

    [System.Serializable]
    public struct VisualSettings
    {
        public Vector3 DirectionalLightRotation;
        public float DirectionalLightIntencity;
        public Color DirectionLightColor;
        public float DirectionalLightDuration;


        public float VignetteIntensity;

        public float PrticlesCount;

    }
}