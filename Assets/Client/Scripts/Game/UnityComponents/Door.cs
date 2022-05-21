using UnityEngine;

namespace Game
{
    public class Door : MonoEntity
    {
        public bool IsLocked;
        public bool IsOpened;
        public Animator Animator;
        public AudioSource Source;
        public AudioClip Open;
        public AudioClip Close;
        public GameObject[] Objects;

        private void OnTriggerEnter(Collider c)
        {
            if(IsLocked || IsOpened) return;

            Animator.SetBool(AniamtionNames.Open, true);
            Source.clip = Open;
            Source.Play();
            IsOpened = true;

            for(var i = 0; i < Objects.Length; i++)
            {
                Objects[i].SetActive(false);
            }
        }

        private void OnTriggerExit(Collider c)
        {
            if(IsLocked || !IsOpened) return;

            Animator.SetBool(AniamtionNames.Open, false);
            Source.clip = Close;
            Source.Play();
            IsOpened = false;

            for(var i = 0; i < Objects.Length; i++)
            {
                Objects[i].SetActive(true);
            }
        }
    }
}