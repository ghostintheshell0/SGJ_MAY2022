using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

namespace Game
{
    public class SceneData : MonoBehaviour
    {
        public Camera MainCamera;
        public CinemachineBrain CinemachineBrain;
        public CinemachineVirtualCamera CinemachineCamera;
        public Player Player {get; set;} 
        public House House;
        public Transform SpawnPoint;
        public Npc Npc;
        public Transform Snowing;

        public GameArea[] GameAreas;

        public MonoEntity[] Entities;
        public AudioClip RespawnClip;
        public FinalCutScene FinalCutScene;
        public bool FollowCamera;
        public bool EnableFootsteps;
        public bool EnableWind;
        public bool ShowNPC;
    
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