#region

using System;
using System.Collections.Generic;
using IbrahKit.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Generic
{
    [Serializable]
    public class UI_Init
    {
        public static void InitSubTree(Transform transform)
        {
            List<IUIInit> subtree = Transform_Utilities.GetComponentsByLevel<IUIInit>(transform, true, false);

            subtree.ForEach(x => x.OnMenuInitTopDown());

            subtree.Reverse();

            subtree.ForEach(x => x.OnMenuInitBottomUp());
        }
    }
}