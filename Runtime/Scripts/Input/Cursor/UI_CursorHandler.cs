using UnityEngine;
using UnityEngine.UI;

namespace IbrahKit
{
    /// <summary>
    /// A component which allows the custom cursor to change states when hovering over this rect transform even without a graphic
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    public class UI_CursorHandler : Graphic, ICursorHandler
    {
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear(); // The line prevents any actual rendering from happening which makes this component appear empty
        }

        public override void SetMaterialDirty() { }
        public override void SetVerticesDirty() { }
    }
}