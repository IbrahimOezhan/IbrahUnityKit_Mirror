#region

using IbrahKit.UI.Generic;
using UnityEngine;

#endregion

public class CustomInit : MonoBehaviour
{
    private void Awake()
    {
        UI_Init.InitSubTree(transform);
    }
}