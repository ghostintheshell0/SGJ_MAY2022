using UnityEngine;
using UnityEngine.Audio;

namespace Game
{
    public class AudioManager : MonoBehaviour
    {
        public AudioMixerGroup AudioGroup;
        public AudioSource Music;
        public AudioSource Wind;


        public float defaultVolume;
    }
}