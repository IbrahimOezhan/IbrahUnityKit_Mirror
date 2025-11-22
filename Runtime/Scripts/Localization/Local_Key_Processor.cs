using IbrahKit;

public class Local_Key_Processor : Key_Reference_Processor<Local_Key_Reference>
{
    public override string GetDBName()
    {
        return "Local";
    }
}
