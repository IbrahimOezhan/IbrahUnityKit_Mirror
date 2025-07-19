using IbrahKit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit
{
    public class ChildSorter : MonoBehaviour
    {
        [Button]
        public void SortChildren()
        {
            Transform_Utilities.SortGameObjects(transform);
        }
    }
}