#region

using System;
using System.Collections.Generic;
using IbrahKit.Localization;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

namespace IbrahKit.Dialog
{
    [Serializable]
    public class Dialog_Sub_Element
    {
        public enum Mode
        {
            SKIPABLE,
            NOTSKIPABLE
        }

        public bool useName = true;

        [SerializeField, ShowIf("useName")] private Local_Key nameKey;

        [SerializeReference] private List<TextProcessor> processors = new();

        [SerializeReference] private Time time;

        [SerializeField] private KeyMode keyMode;

        [SerializeField, ShowIf("keyMode", KeyMode.NORMAL)]
        private Local_Key contentKey;

        [SerializeField, ShowIf("keyMode", KeyMode.RANDOM)]
        private Local_Key[] randomKeys;

        [SerializeField] private Mode mode;

        public Mode GetMode() => mode;

        public float GetTime(char c) => time.GetDelay(c);

        public bool TryName(out string name)
        {
            name = string.Empty;

            if (nameKey != null) name = Local_Manager.GetInstance().GetString(nameKey);

            return useName;
        }

        public Local_Key GetKey()
        {
            return keyMode switch
            {
                KeyMode.NORMAL => contentKey,
                KeyMode.RANDOM => randomKeys[Random.Range(0, randomKeys.Length)],
                _ => null,
            };
        }

        public string Process(string text)
        {
            string result = text;

            foreach (var processor in processors)
            {
                processor.Process(result);
            }

            return result;
        }

        private enum KeyMode
        {
            NORMAL,
            RANDOM
        }
    }
}