using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit
{
    public class Transform_Child_Organizer : MonoBehaviour
    {
        [Button]
        public void SortChildren()
        {
            transform.SortChildren();
        }
    }
}