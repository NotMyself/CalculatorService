using System;
using System.Linq;
using CalculatorService.Service;
using log4net;
using Topshelf;

//this call is global causes log4net to configure using app.config
[assembly:log4net.Config.XmlConfigurator]

namespace CalculatorService
{
    public class Program
    {
        const string DefaultServiceUri = "http://localhost:8080/calc";

        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {

            var serviceUri = args.Any() && Uri.IsWellFormedUriString(args[0], UriKind.RelativeOrAbsolute) 
                ? args[0] : DefaultServiceUri;

            Logger.Info($"Service Url: {serviceUri}");

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
