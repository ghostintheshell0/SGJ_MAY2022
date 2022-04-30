using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/Crap data")]
    public class CrapData : ScriptableObject
    {
        public Sprite Icon;
        public Crap Prefab;
    }
}