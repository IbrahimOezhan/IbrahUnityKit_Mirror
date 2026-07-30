public interface ISavePipeline
{
    public string OnDeserialize(string data);

    public string OnSerialize(string data);
}