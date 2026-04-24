#region

using System.Collections.Generic;
using System.Text;
using IbrahKit.Debugging;
using UnityEngine;

#endregion

namespace IbrahKit.Utilities
{
    public static class Transform_Utilities
    {
        public static string GetTransformPath(this Transform transform)
        {
            StringBuilder result = new(transform.name);

            Transform parent = transform.parent;

            while (parent != null)
            {
                result.Insert(0, parent.name + "/");

                parent = parent.parent;
            }

            return result.ToString();
        }

        public static List<T> GetComponentsByLevel<T>(this Transform root, bool includeThis = false,
            bool bottomFirst = false)
        {
            List<T> result = new();
            if (root == null)
                return result;

            Queue<Transform> queue = new();

            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Transform current = queue.Dequeue();

                if (includeThis || current != root)
                {
                    result.AddRange(current.GetComponents<T>());
                }

                for (int i = 0; i < current.childCount; i++)
                {
                    queue.Enqueue(current.GetChild(i));
                }
            }

            if (bottomFirst) result.Reverse();

            return result;
        }


        public static List<T> BetterGetComponentsInChildren<T>(this Transform transform, bool includeThis = false)
        {
            if (transform == null)
            {
                IbrahDebug.LogWarning("Transform is null");

                return new(0);
            }

            List<T> elements = new();

            if (includeThis)
            {
                elements.AddRange(transform.GetComponents<T>());
            }

            foreach (Transform child in transform)
            {
                T[] compArray = child.GetComponents<T>();

                foreach (T comp in compArray)
                {
                    elements.Add(comp);
                }

                if (child.childCount > 0)
                {
                    elements.AddRange(child.BetterGetComponentsInChildren<T>());
                }
            }

            return elements;
        }

        public static bool BetterTryGetComponentInParent<T>(this Transform transform, out T result,
            bool includeThis = false)
        {
            result = transform.BetterGetComponentInParent<T>(includeThis);

            return result != null;
        }

        public static T BetterGetComponentInParent<T>(this Transform transform, bool includeThis = false)
        {
            if (includeThis && transform.TryGetComponent<T>(out T result))
            {
                return result;
            }

            if (transform.parent == null)
            {
                return default;
            }

            if (transform.parent.TryGetComponent<T>(out var element))
            {
                return element;
            }

            return transform.parent.BetterGetComponentInParent<T>(false);
        }

        public static T[] BetterGetComponentsInParents<T>(this Transform transform, bool includeThis = false)
        {
            List<T> result = new();

            if (includeThis && transform.TryGetComponent<T>(out var element)) result.Add(element);

            return transform.BetterGetComponentsInParent(result);
        }

        private static T[] BetterGetComponentsInParent<T>(this Transform transform, List<T> result)
        {
            Transform parent = transform.parent;

            if (parent == null)
            {
                return result.ToArray();
            }

            if (parent.TryGetComponent<T>(out var element))
            {
                result.Add(element);
            }

            return parent.BetterGetComponentsInParent<T>(result);
        }

        public static Quaternion GetRotation(Transform _transformToRotate, Transform _rotateTarget, float _offset)
        {
            var _heading = _rotateTarget.position - _transformToRotate.position;

            var _heading2d = new Vector2(_heading.x, _heading.z).normalized;

            var _angle = Mathf.Atan2(_heading2d.y, _heading2d.x) * -Mathf.Rad2Deg + _offset;

            return Quaternion.AngleAxis(_angle, Vector3.up);
        }

        public static void SortChildren(this Transform parent)
        {
            List<GameObject> children = new();

            foreach (Transform child in parent)
            {
                children.Add(child.gameObject);
            }

            SortObjects(children);
        }

        public static void SortObjects(List<GameObject> children)
        {
            if (children == null)
            {
                IbrahDebug.LogWarning("List is null");

                return;
            }

            if (children.Count == 0)
            {
                IbrahDebug.LogWarning("List is empty");

                return;
            }

            children.Sort((GameObject one, GameObject two) => { return one.name.CompareTo(two.name); });

            for (int i = 0; i < children.Count; i++)
            {
                children[i].transform.SetSiblingIndex(i);
            }
        }
    }
}