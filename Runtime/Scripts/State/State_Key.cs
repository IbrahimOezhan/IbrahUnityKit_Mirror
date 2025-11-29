namespace IbrahKit
{
    public class State_Key : Key_Reference
    {
        private class State_Key_Processor : Key_Processor<State_Key>
        {
            public override string GetDBName()
            {
                return State_Manager.KEY;
            }
        }
    }
}