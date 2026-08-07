#region

using IbrahKit.Dialog;
using UnityEngine;

#endregion

[CreateAssetMenu(menuName = "IbrahKit/ScriptableObjects/Dialog Playable Element",
    fileName = "NewDialogPlayableElements")]
public class Dialog_Playable_Elements : ScriptableObject
{
    [SerializeField] private SimpleDialogElement[] elements;

    public SimpleDialogElement[] GetElements() => elements;
}