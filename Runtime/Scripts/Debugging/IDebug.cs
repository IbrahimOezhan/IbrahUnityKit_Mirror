using UnityEngine;

namespace IbrahKit
{
    public interface IDebug
    {
        GameObject gameObject { get; }

        public string Run();

        public int Order();
    }
}