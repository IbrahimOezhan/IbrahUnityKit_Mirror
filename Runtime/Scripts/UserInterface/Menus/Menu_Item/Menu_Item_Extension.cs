using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public abstract class Menu_Item_Extension
    {
        protected GameObject spawnedObject;

        public GameObject GetSpawnedObject()
        {
            return spawnedObject;
        }

        public abstract bool Spawn(RectTransform parent, UI_Menu menu);
    }
}