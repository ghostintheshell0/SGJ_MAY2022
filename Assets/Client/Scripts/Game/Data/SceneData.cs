using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

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

        [Button]
        public void Grab()
        {
            Entities = GameObject.FindObjectsOfType<MonoEntity>();
        }
    }

}