#region

using Sirenix.OdinInspector;

#endregion

namespace IbrahKit.Core
{
    public interface IFileWatcher
    {
        [Button]
        public void OnFileUpdaate();
    }
}