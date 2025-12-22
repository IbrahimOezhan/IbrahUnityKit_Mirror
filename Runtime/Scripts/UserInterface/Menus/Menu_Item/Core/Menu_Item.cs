using UnityEngine;

namespace IbrahKit.UI
{
    [System.Serializable]
    public abstract class Menu_Item<T> : Menu_Item_Base where T : Component
    {
        [SerializeReference] private T prefab;
    }
}