using System;

public class SaveInUseException : Exception
{
    public SaveInUseException()
         : base("Could not load savable: it's already in use by another object. Unload it first using Save_Manager.Instance.Return before loading it elsewhere.") { }
}
