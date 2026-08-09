#region

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using IbrahKit.Core;
using IbrahKit.Manager;
using IbrahKit.Save;
using Sirenix.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.Unlockables
{
    /// <summary>
    ///     Wrapper on top of the save system for saving persistent unlocks.
    ///     Can be used for things such as collectables
    /// </summary>
    [DefaultExecutionOrder(Execution_Order.unlock)]
    public class Unlockables_Manager : MonoBehaviourSingletonDontDestroyOnLoad<Unlockables_Manager>
    {
        [SerializeField] private SaveData saveData = new();

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            saveData = Save_Manager.GetInstance().GetLoadedSave().Get<SaveData>();
        }

        public void Unlock(IEnumerable<Unlockable> unlockable)
        {
            unlockable.ForEach(Unlock);
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

        [Serializable]
        private class SaveData : ISavable
        {
            [JsonInclude] [SerializeField] private List<string> unlockedUnlockables = new();

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