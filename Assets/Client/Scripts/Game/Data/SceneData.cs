using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Cinemachine;

namespace Game
{
    public class SceneData : MonoBehaviour
    {
        public Camera MainCamera;
        public CinemachineBrain CinemachineBrain;
        public CinemachineVirtualCamera CinemachineCamera;
        public Player Player;
        public Npc Npc;

        public GameArea[] GameAreas;

        public MonoEntity[] Entities;

        public AudioMixerGroup AudioGroup;
        public AudioSource AudioSource;
        public AudioClip RespawnClip;


    
        [Button]
        public void Grab()
        {
            Entities = GameObject.FindObjectsOfType<MonoEntity>();
        }

        public void Restart()
        {
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