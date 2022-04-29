using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Game
{
    public class SceneData : MonoBehaviour
    {
        public Camera MainCamera;

        public MonoEntity[] Entities;

        [Button]
        public void Grab()
        {
            Entities = GameObject.FindObjectsOfType<MonoEntity>();
        }
    }
}