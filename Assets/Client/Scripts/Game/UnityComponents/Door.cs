using UnityEngine;

namespace Game
{
    public class Door : MonoEntity
    {
        public House House;
        public Animator Animator;
        public AudioSource Source;
        public AudioClip Open;
        public AudioClip Close;

        private void OnTriggerEnter(Collider c)
        {
            if(House.IsLocked) return;

            var isOpened = Animator.GetBool(AniamtionNames.Open);
            if(!isOpened)
            {
                Animator.SetBool(AniamtionNames.Open, true);
                Source.clip = Open;
                Source.Play();
            }
        }

        private void OnTriggerExit(Collider c)
        {
            if(House.IsLocked) return;

            var isOpened = Animator.GetBool(AniamtionNames.Open);
            if(isOpened)
            {
                Animator.SetBool(AniamtionNames.Open, false);
                Source.clip = Close;
                Source.Play();
            }
            
        }
    }
}