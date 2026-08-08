#region

using System;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    [Serializable]
    public abstract class Menu_Item<T> : Menu_Item_Base where T : Component
    {
        [SerializeReference] private T prefab;
    }
}