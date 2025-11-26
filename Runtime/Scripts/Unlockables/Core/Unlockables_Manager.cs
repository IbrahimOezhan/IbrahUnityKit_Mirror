using IbrahKit.Save;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;

namespace IbrahKit.Unlockables
{
    [DefaultExecutionOrder(Execution_Order.unlock)]
    public class Unlockables_Manager : Manager_DDOL<Unlockables_Manager>
    {
        private const string SAVE_DATA_NAME = "Unlockables";

        [SerializeField] private SaveData saveData = new();

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            Save_Manager.GetInstance().TryLoad(SAVE_DATA_NAME, out saveData);
        }

        private void OnDestroy()
        {
            if (GetInstance() == this)
            {
                if (Save_Manager.TryGet(out Save_Manager result))
                {
                    result.Return(SAVE_DATA_NAME, saveData);
                }
            }
        }

        public void Unlock(IEnumerable<Unlockable> unlockable)
        {
            unlockable.ForEach(x => Unlock(x));
        }

        public void Unlock(Unlockable unlockable)
        {
            Unlock(unlockable.GetKey());
        }

        public void Unlock(string key)
        {
            saveData.Unlock(key);
        }

        public bool IsUnlocked(string key)
        {
            return saveData.IsUnlocked(key);
        }

        [System.Serializable]
        private class SaveData : Savable
        {
            [JsonInclude][SerializeField] private List<string> unlockedUnlockables = new();

            public bool IsUnlocked(string key)
            {
                return unlockedUnlockables.Contains(key);
            }

            public void Unlock(string key)
            {
                if (!unlockedUnlockables.Contains(key))
                {
                    unlockedUnlockables.Add(key);
                }
            }
        }
    }
}