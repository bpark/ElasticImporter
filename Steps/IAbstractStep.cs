namespace ElasticImporter.Steps
{
    public interface IAbstractStep
    {
        void Execute();

        string Name();
    }
}