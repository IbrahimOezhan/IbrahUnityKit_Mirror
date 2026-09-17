#region

using System;
using System.Text.Json.Serialization;
using IbrahKit.Save;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

#endregion

namespace IbrahKit
{
    internal class Sample_Save : MonoBehaviour
    {
        [SerializeField] private Text text;

        private SaveData save;

        private void Start()
        {
            save = Save_Manager.GetInstance().GetLoadedSave().Get<SaveData>();
        }

        private void Update()
        {
            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                save.value--;
            }
            else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                save.value++;
            }
            
            text.text = save.value.ToString();
        }

        [Serializable]
        private class SaveData : ISavable
        {
            [JsonInclude] public float value;
        }
    }
}