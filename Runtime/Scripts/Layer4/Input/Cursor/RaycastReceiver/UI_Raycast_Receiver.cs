#region

#endregion

namespace IbrahKit.Input
{
# if UNITY_6000_5_OR_NEWER

# else // Not required in 6.5 or Newer. Use the native Raycast_Receiver instead 
    /// <summary>
    ///     A component which allows the custom cursor to change states when hovering over this rect transform even without a
    ///     graphic
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    public class UI_Raycast_Receiver : Graphic, ICursorHandler
    {
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear(); // The line prevents any actual rendering from happening which makes this component appear empty
        }

        public override void SetMaterialDirty()
        {
        }

        public override void SetVerticesDirty()
        {
        }
    }
# endif
}