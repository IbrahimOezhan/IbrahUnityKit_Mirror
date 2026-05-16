#region

using System.Collections.Generic;

#endregion

namespace IbrahKit.Dialog
{
    public abstract class Time
    {
        public float GetDelay(char text)
        {
            return GetDelayPro(text) * SpecialCharDelay(text);
        }

        protected abstract float GetDelayPro(char text);

        private float SpecialCharDelay(char _char)
        {
            if (delayChars.TryGetValue(_char, out int _delay)) return _delay;
            return 1;
        }

        private readonly Dictionary<char, int> delayChars = new()
        {
        {',' , 3},
        {'.' , 4},
        {'!' , 4},
        {'?' , 4}
        };
    }
}