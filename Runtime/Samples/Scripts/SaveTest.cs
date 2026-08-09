using System;
using System.Text.Json.Serialization;
using IbrahKit.Save;
using UnityEngine;
using UnityEngine.UI;

namespace IbrahKit
{
    public class SaveTest : MonoBehaviour
    {
        public Text text;

        private SaveData save;
        
        private void Start()
        {
           save = Save_Manager.GetInstance().GetLoadedSave().Get<SaveData>();

           text.text = save.value.ToString();
        }

        public void Increase()
        {
            save.value++;
            text.text = save.value.ToString();
        }

        public void Decrease()
        {
            save.value--;
            text.text = save.value.ToString();
        }

        [Serializable]
        private class SaveData : ISavable
        {
            [JsonInclude]
            public float value;
        }
    }
}
