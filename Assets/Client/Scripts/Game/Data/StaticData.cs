using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/StaticData")]
    public class StaticData : ScriptableObject
    {
        [Header("Raycast input")]
        public float InputRayDistance;
        public int RaycastResylts;
        public LayerMask InputLayers;
        public LayerMask InteractiveObjectsLayer;
        public LayerMask GroundLayers;

        [Space(20)][Header("Gameplay data")]
        public  CrapData[] AllCrap;
        public bool RespawnCrap;
    }
}