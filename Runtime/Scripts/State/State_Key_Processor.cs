namespace IbrahKit
{
    public class State_Key_Processor : Key_Reference_Processor<State_Key>
    {
        public override string GetDBName()
        {
            return "States";
        }
    }
}