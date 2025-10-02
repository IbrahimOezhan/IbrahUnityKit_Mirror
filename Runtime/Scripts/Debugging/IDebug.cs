using UnityEngine;

namespace IbrahKit
{
    public interface IDebug
    {
        GameObject gameObject { get; }

        public string DebugContent();

        public int DebugOrder();
    }
}