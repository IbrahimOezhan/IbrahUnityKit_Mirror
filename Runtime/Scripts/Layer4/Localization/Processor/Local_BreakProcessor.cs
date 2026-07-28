namespace IbrahKit.Localization
{
    /// <summary>
    ///     Replaces [Break] with a new line
    /// </summary>
    public class Local_BreakProcessor : Local_Processor
    {
        public override string Process(string input)
        {
            return input.Replace("[Break]", "\n");
        }
    }
}