using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public abstract class ScriptableCollection<T> : ScriptableObject
    {
        public List<T> Collection;
    }
}