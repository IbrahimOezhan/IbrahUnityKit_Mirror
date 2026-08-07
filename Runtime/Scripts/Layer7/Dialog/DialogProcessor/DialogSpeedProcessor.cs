#region

using System;

#endregion

namespace IbrahKit.Dialog
{
    [Serializable, DialogTag("Speed")]
    public abstract class DialogSpeedProcessor : DialogProcessor
    {
        /*
        [SerializeField] private Local_Key key;

        public string GetString()
        {
            return Local_Manager.GetInstance().GetString(key);
        }

        private readonly Dictionary<char, int> delayChars = new()
        {
            { ',', 3 },
            { '.', 4 },
            { '!', 4 },
            { '?', 4 }
        };

        public float GetDelay(string raw, List<SimpleDialogElement.Token> tokens)
        {
            return GetDelayPro(c, text) * SpecialCharDelay(c);
        }

        protected abstract float GetDelayPro(char c, string text);

        private float SpecialCharDelay(char _char)
        {
            return delayChars.GetValueOrDefault(_char, 1);
        }*/
    }
}