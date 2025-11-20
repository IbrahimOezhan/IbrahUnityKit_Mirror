using IbrahKit.Localization;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit
{
    [CreateAssetMenu(fileName = "NewUnlockable", menuName = "IbrahKit/Unlockable")]
    public class Unlockable : ScriptableObject
    {
        [TabGroup("Base Data"), Dropdown(Local_Manager.DROP), SerializeField]
        protected string key;

        [TabGroup("Base Data"), SerializeField]
        private Unlockable[] unlockOnUnlock;

        public virtual void Unlock()
        {
            if (IsUnlocked()) return;

            if (unlockOnUnlock != null)
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

            if (Unlockables_Manager.TryGet(out Unlockables_Manager result))
            {
                Unlockables_Manager.GetInstance().Unlock(this);
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

        public string GetKey()
        {
            return key;
        }
    }
}