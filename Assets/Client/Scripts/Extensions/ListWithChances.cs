using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    [System.Serializable]
    public class ListWithChances<T>
    {
        [SerializeField]
        private List<ItemWithChance<T>> _list = new List<ItemWithChance<T>>();

        private float _total = 0;

        public T Get()
        {
            var r = Random.Range(0, _total);
            float current = 0;

            foreach(var pair in _list)
            {
                current += pair.Chance;
                if(current >= r)
                {
                    return pair.Item;
                }

            }

            throw new System.Exception();
        }


        ///
        ///r Э [0f, 1f];
        public T Get(float r)
        {
            r *= _total;
            float current = 0;

            foreach(var pair in _list)
            {
                current += pair.Chance;
                if(current >= r)
                {
                    return pair.Item;
                }

            }

//            return _list.GetRandom().Item;
            throw new System.Exception();
        }

        public void AddChance(T item, float chance)
        {
            var index = IndexOf(item);
            if(index >= 0)
            {
                var newChance = _list[index].Chance + chance;

                if(newChance < 0)
                {
                    throw new System.Exception();
                }

                _list[index] = new ItemWithChance<T>(item, newChance);
                _total += chance;
                return;
            }

            _list.Add(new ItemWithChance<T>(item, chance));
            _total += chance;
            
        }

        public void Remove(T item)
        {
            var index = IndexOf(item);
            if(index >= 0)
            {
                _total -= _list[index].Chance;
                _list.RemoveAt(index);
            }
        }

        public void SetChance(T item, float chance)
        {
            if(chance < 0)
            {
                throw new System.Exception();
            }
            
            var index = IndexOf(item);
            if(index >= 0)
            {
                _total -= _list[index].Chance;
                _list[index] = new ItemWithChance<T>(item, chance);
                _total += chance;
            }
            else
            {
                _list.Add(new ItemWithChance<T>(item, chance));
                _total += chance;
            }
        }

        private int IndexOf(T item)
        {
            for(var i = 0; i < _list.Count; i++)
            {
                if(_list[i].Item.Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Calculate()
        {
            _total = 0;

            for(var i = 0; i < _list.Count; i++)
            {
                _total += _list[i].Chance;
            }
        }
    }


    [System.Serializable]
    public struct ItemWithChance<T>
    {
        public T Item;
        public float Chance;

        public ItemWithChance(T item, float chance)
        {
            Item = item;
            Chance = chance;
        }
    }
}