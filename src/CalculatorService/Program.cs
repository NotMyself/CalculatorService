using CalculatorService.Service;
using log4net;
using Topshelf;

[assembly:log4net.Config.XmlConfigurator]

namespace CalculatorService
{
    class Program
    {
        private static ILog logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            const string serviceUri = "http://localhost:8080/calc";

            logger.Info($"Service Url: {serviceUri}");

            var host = HostFactory.New(c =>
            {
                c.Service<WcfServiceWrapper<Calculator, ICalculator>>(s =>
                {
                    s.ConstructUsing(x => 
                        new WcfServiceWrapper<Calculator, ICalculator>("Calculator", serviceUri));
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
