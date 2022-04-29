using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

namespace Game
{
    public class MonoEntity : MonoBehaviour
    {
        public EcsEntity Entity;
        public EcsWorld World;

        public void Init(EcsWorld world)
        {
            World = world;
            Entity = World.NewEntity();
        }

        protected virtual void OnInit()
        {
            ref var t = ref Entity.Get<TransformRef>();
            t.Value = transform;
        }
    }
}
