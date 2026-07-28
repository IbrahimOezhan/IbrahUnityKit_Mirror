#region

using IbrahKit.Debugging;
using IbrahKit.Keys;
using IbrahKit.Localization;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.Unlockables
{
    [CreateAssetMenu(fileName = "NewUnlockable", menuName = "IbrahKit/Unlockable")]
    public class Unlockable : ScriptableObject, IKey
    {
        [TabGroup("Base Data"), SerializeField]
        protected Local_Key key;

        [TabGroup("Base Data"), SerializeField]
        private Unlockable[] unlockOnUnlock;

        public string GetKey()
        {
            return key;
        }

        public virtual void Unlock()
        {
            if (IsUnlocked()) return;

            if (unlockOnUnlock != null)
            {
                UnlockOnUnlock();
            }

            if (Unlockables_Manager.TryGet(out Unlockables_Manager result))
            {
                Unlockables_Manager.GetInstance().Unlock(this);
            }
        }

        private void UnlockOnUnlock()
        {
            for (int i = 0; i < unlockOnUnlock.Length; i++)
            {
                if (unlockOnUnlock[i] == null)
                {
                    IbrahDebug.LogWarning(nameof(unlockOnUnlock) + " contains null values");
                    continue;
                }

                unlockOnUnlock[i].Unlock();
            }
        }

        public bool IsUnlocked()
        {
            if (Unlockables_Manager.TryGet(out Unlockables_Manager result))
            {
                return result.IsUnlocked(key);
            }

            return false;
        }
    }
}