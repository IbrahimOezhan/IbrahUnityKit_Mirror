using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace IbrahKit
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

        public static List<T> BetterGetComponentsInChildren<T>(this Transform transform, bool includeThis = false)
        {
            if (transform == null)
            {
                Debug.LogWarning("Transform is null");
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

        public static T BetterGetComponentInParent<T>(this Transform transform)
        {
            if (transform.parent == null)
            {
                return default;
            }

            if (transform.parent.TryGetComponent<T>(out var element))
            {
                return element;
            }

            return transform.parent.BetterGetComponentInParent<T>();
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
                Debug.LogWarning("List is null");

                return;
            }

            if (children.Count == 0)
            {
                Debug.LogWarning("List is empty");

                return;
            }

            children.Sort((GameObject one, GameObject two) =>
            {
                return one.name.CompareTo(two.name);
            });

            for (int i = 0; i < children.Count; i++)
            {
                children[i].transform.SetSiblingIndex(i);
            }
        }
    }
}