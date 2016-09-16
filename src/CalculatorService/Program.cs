using CalculatorService.Service;
using Topshelf;

namespace CalculatorService
{
    class Program
    {
        static void Main(string[] args)
        {
            const string serviceUri = "http://localhost:8080/calc";

            var host = HostFactory.New(c =>
            {
                c.Service<WcfServiceWrapper<Calculator, ICalculator>>(s =>
                {
                    s.ConstructUsing(x => new WcfServiceWrapper<Calculator, ICalculator>("Calculator", serviceUri));
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });
                c.RunAsLocalSystem();

                c.SetDescription("Runs CalculatorService.");
                c.SetDisplayName("CalculatorService");
                c.SetServiceName("CalculatorService");
            });

            host.Run();
        }
    }
}
