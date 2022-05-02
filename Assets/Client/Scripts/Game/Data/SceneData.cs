using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace Game
{
    public class SceneData : MonoBehaviour
    {
        public Camera MainCamera;
        public Player Player;
        public Npc Npc;

        public GameArea[] GameAreas;

        public Image OutGameAreaWarning;

        public MonoEntity[] Entities;

        public AudioMixerGroup AudioGroup;
        public Slider AudioSlider;
        public AudioSource AudioSource;
        public AudioClip RespawnClip;

        public FinalCutScene FinalCutScene;

    
        [Button]
        public void Grab()
        {
            Entities = GameObject.FindObjectsOfType<MonoEntity>();
        }
    }

}