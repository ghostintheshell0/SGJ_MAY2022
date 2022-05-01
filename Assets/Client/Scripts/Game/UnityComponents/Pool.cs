using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Pool<T> : MonoEntity where T : Component
    {
        private List<T> pool;
        [SerializeField] private T prefab;

        protected override void OnInit()
        {
            pool = new List<T>();
        }

        public T Get()
        {
            if(pool.Count > 0)
            {
                var item = pool[pool.Count-1];
                pool.RemoveAt(pool.Count-1);
                item.transform.SetParent(default);
                item.gameObject.SetActive(true);
                return item;
            }

            return CreateItem();
        }

        public void Recycle(T item)
        {
                item.transform.SetParent(transform);
            item.gameObject.SetActive(false);
            pool.Add(item);
        }

        private T CreateItem()
        {
            var item = Instantiate(prefab);
            return item;
        }
    }
}