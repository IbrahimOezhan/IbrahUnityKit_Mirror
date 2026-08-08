#region

using IbrahKit.Debugging;
using IbrahKit.Keys;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.Unlockables
{
    [CreateAssetMenu(fileName = "NewUnlockable", menuName = "IbrahKit/Unlockable")]
    public class Unlockable : ScriptableObject, IKey
    {
        [TabGroup("Base Data"), SerializeField]
        protected string key;

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
            foreach (var unlockable in unlockOnUnlock)
            {
                if (unlockable == null)
                {
                    IbrahDebug.LogWarning(nameof(unlockOnUnlock) + " contains null values");
                    continue;
                }

                unlockable.Unlock();
            }
        }

        public bool IsUnlocked()
        {
            return Unlockables_Manager.TryGet(out Unlockables_Manager result) && result.IsUnlocked(key);
        }
    }
}