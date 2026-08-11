#region

using System;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    [Serializable]
    public abstract class UI_Menu_Transition_Time : UI_Menu_Transition
    {
        [SerializeField] protected float time;
    }
}