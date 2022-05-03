using System;
using UnityEngine;

namespace Game
{
    public struct PlayerSpawnerComponent
    {
        public Transform Point;
        public string Name;
        public Action<Player> OnSpawn;

    }
}