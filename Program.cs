using Serilog;
using ElasticImporter.Steps;

namespace ElasticImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            
            var steps = new IAbstractStep[] {new DataReadStep(), new ImportStep()};

            foreach (var step in steps)
            {
                Log.Information($"executing step {step.Name()}");
                step.Execute();                
            }
        }
    }
}