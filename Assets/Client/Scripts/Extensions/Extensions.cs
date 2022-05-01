using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Extensions
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this List<T> list)
        {
            for(var i = 0; i < list.Count; i++)
            {
                Swap(list, i, Random.Range(0, list.Count));
            }
        }
        
        public static void Swap<T>(this List<T> list, int a, int b)
        {
            var temp = list[a];
            list[a] = list[b];
            list[b] = temp;
        }

        public static T GetRandom<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static T Extract<T>(this List<T> list, int id)
        {
            var e = list[id];
            list.RemoveAt(id);
            return e;
        }

        public static T ExtractRandom<T>(this List<T> list)
        {
            var id = Random.Range(0, list.Count);
            var e = list[id];
            list.RemoveAt(id);
            return e;
        }
    }

    public static class TransformExtensions
    {
        public static bool IsNear(this Transform t, Transform target, float distance)
        {
            return (t.position - target.position).sqrMagnitude <= distance * distance;
        }

        public static bool IsNear(this Transform t, Vector3 target, float distance)
        {
            return (t.position - target).sqrMagnitude <= distance * distance;
        }

        public static bool IsNear(this Transform t, Vector2 target, float distance)
        {
            var pos = new Vector2(t.position.x, t.position.y);
            return (pos - target).sqrMagnitude <= distance * distance;
        }

        //set negative scale x if derection less than zero
        public static void SetDirection(this Transform t, float direction)
        {
            var scale = t.localScale;
            scale.x = direction < 0 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
            t.localScale = scale;
        }
    }

    public static class GameObjectExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            var c = go.GetComponent<T>();
            if(c == default)
            {
                c = go.AddComponent<T>();
            }

            return c;
        }
    }

    public static class SpriteRendererExtensions
    {
        public static void SetAlpha(this SpriteRenderer r, float alpha)
        {
            var c = r.color;
            c.a = alpha;
            r.color = c;
        }
    }

    public static class ImageExtensions
    {
        public static void SetAlpha(this Image image, float alpha)
        {
            var c = image.color;
            c.a = alpha;
            image.color = c;
        }
    }

    public static class LayerMaskExtensions
    {
        public static bool Contains(this LayerMask mask, LayerMask other)
        {
            return (mask & 1 << other) != 0;
        }
    }

    public static class RandomUtils
    {
        public static int Roll(int dices, int edges)
        {
            var result = 0;

            for(var i = 0; i < dices; i++)
            {
                result += Random.Range(0, edges-1)+1;
            }

            return result;
        }

        public static bool Bool()
        {
            return Random.Range(0f, 1f) < 0.5f;
        }

        public static Vector2 RandomVector2(Vector2 min, Vector2 max)
        {
            var v = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            return v;
        }

        public static Vector2 RandomVector2(Vector3 min, Vector3 max)
        {
            var v = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            return v;
        }


        public static Vector2 RandomVector2(Rect rect)
        {
            var v = new Vector2(Random.Range(rect.min.x, rect.max.x), Random.Range(rect.min.y, rect.max.y));
            return v;
        }

        public static Vector3 RandomVector3(Vector3 min, Vector3 max)
        {
            var v = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
            return v;
        }

        public static Vector3 RandomVector3(Bounds bounds)
        {
            var v = new Vector3(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y), Random.Range(bounds.min.z, bounds.max.z));
            return v;
        }
    }

    public static class IntExtensions
    {
        public static bool IsInRange(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }
    }

    public static class FloatExtensions
    {
        public static bool IsInRange(this float value, float min, float max)
        {
            return value >= min && value <= max;
        }
    }

    public static class RectTransformExtensions
    {

        //negative values for t.localScale not supported :(
        public static void Clamp(this RectTransform t, Rect bounds)
        {
            var scale = t.localScale;

            var size = t.rect.size;
            var halfSize = size * t.pivot;
            var minPos = bounds.min + (halfSize * scale);
            var maxPos = bounds.max - (size - halfSize) * scale;

            var pos = t.anchoredPosition;
            pos.x = Mathf.Clamp(pos.x, minPos.x, maxPos.x);
            pos.y = Mathf.Clamp(pos.y, minPos.y, maxPos.y);

            t.anchoredPosition = pos;
        }
    }
}