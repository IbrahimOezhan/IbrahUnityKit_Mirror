using IbrahKit.Localization;
using UnityEngine;

public class Local_UITK_SO : MonoBehaviour
{
    [SerializeField] private Local_Key key;

    public string GetKey() => key;
}
