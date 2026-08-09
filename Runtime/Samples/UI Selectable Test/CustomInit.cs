using System;
using IbrahKit.UI.Generic;
using UnityEngine;

public class CustomInit : MonoBehaviour
{
    private void Awake()
    {
        UI_Init.InitSubTree(transform);
    }
}
