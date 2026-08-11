#region

using IbrahKit.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.Dialog
{
    public class Zalgo_Processor : DialogProcessor
    {
        [SerializeField] private int intensity;

        public override string Process(string text)
        {
            return String_Utilities.GenerateZalgoText(text, intensity);
        }
    }
}