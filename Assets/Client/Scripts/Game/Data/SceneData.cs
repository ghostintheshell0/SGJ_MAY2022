using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

namespace Game
{
    public class SceneData : MonoBehaviour
    {
        public KeyCode RespawnCrapButton;
        public Camera MainCamera;
        public CinemachineBrain CinemachineBrain;
        public CameraFollow CameraFollow;
        public CinemachineVirtualCamera CinemachineCamera;
        public Character Player {get; set;} 
        
        public Transform SpawnPoint;
        
        public Transform Snowing;

        public GameArea[] GameAreas;

        public MonoEntity[] Entities;
        public AudioClip RespawnClip;
        public FinalCutScene FinalCutScene;
        public bool FollowCamera;
        public bool EnableFootsteps;
        public bool EnableWind;
    
        [Button]
        public void Grab()
        {
            Entities = GameObject.FindObjectsOfType<MonoEntity>();
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