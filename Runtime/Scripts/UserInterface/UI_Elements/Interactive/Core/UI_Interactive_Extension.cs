using UnityEngine;

namespace IbrahKit
{
    [RequireComponent(typeof(UI_Interactive)), AddComponentMenu("")]
    public abstract class UI_Interactive_Extension : Extension
    {
        protected UI_Interactive_Extension(GameObject go) : base(go)
        {

        }
    }
}