using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public abstract class Menu_Item<T> : Menu_Item_Base where T : Component
    {
        [SerializeReference] private T prefab;
    }
}