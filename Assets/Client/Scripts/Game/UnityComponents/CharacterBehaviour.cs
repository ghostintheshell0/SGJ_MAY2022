using UnityEngine;

namespace Game
{
    public abstract class CharacterBehaviour : MonoBehaviour
    {
        public Character Character {get; private set;}

        public void Init(Character character)
        {
            this.Character = character;
            OnInit();
        }

        protected abstract void OnInit();
    }
}