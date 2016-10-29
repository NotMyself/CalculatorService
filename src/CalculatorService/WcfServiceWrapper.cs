using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.ServiceProcess;
using log4net;

namespace CalculatorService
{
    public class WcfServiceWrapper<TServiceImplementation, TServiceContract> : ServiceBase
        where TServiceImplementation : TServiceContract
    {
        private readonly string _serviceUri;
        private ServiceHost _serviceHost;

        public WcfServiceWrapper(string serviceName, string serviceUri)
        {
            _serviceUri = serviceUri;
            ServiceName = serviceName;
            Logger = LogManager.GetLogger(typeof(WcfServiceWrapper<TServiceImplementation, TServiceContract>));
        }

        public ILog Logger { get; set; }

        protected override void OnStart(string[] args)
        {
            Start();
        }

        protected override void OnStop()
        {
            Stop();
        }

        public void Start()
        {
            Logger?.Info($"{ServiceName} starting.");
            var openSucceeded = false;
            try
            {
                _serviceHost?.Close();

                _serviceHost = new ServiceHost(typeof(TServiceImplementation));
            }
            catch (Exception e)
            {
                Logger?.Error($"Caught exception while creating {ServiceName}:{e}", e);
                return;
            }

            try
            {
                var webHttpBinding = new WebHttpBinding(WebHttpSecurityMode.None);
                _serviceHost.AddServiceEndpoint(typeof(TServiceContract), webHttpBinding, _serviceUri);

                var webHttpBehavior = new WebHttpBehavior
                {
                    DefaultOutgoingResponseFormat = WebMessageFormat.Json
                };
                _serviceHost.Description.Endpoints[0].Behaviors.Add(webHttpBehavior);

                _serviceHost.Open();
                openSucceeded = true;
            }
            catch (Exception ex)
            {
                Logger?.Error($"Caught exception while starting {ServiceName} : {ex}", ex);
            }
            finally
            {
                if (!openSucceeded)
                {
                    _serviceHost.Abort();
                }
            }

            if (_serviceHost.State == CommunicationState.Opened)
            {
                Logger?.Info($"{ServiceName} started at {_serviceUri}.");
            }
            else
            {
                Logger?.Fatal($"{ServiceName} failed to open.");
                var closeSucceeded = false;
                try
                {
                    _serviceHost.Close();
                    closeSucceeded = true;
                }
                catch (Exception ex)
                {
                    Logger?.Error($"{ServiceName} failed to close: {ex}", ex);
                }
                finally
                {
                    if (!closeSucceeded)
                    {
                        _serviceHost.Abort();
                    }
                }
            }
        }

        public new void Stop()
        {
            Logger?.Info($"{ServiceName} stopping.");
            try
            {
                if (_serviceHost == null) return;
                _serviceHost.Close();
                _serviceHost = null;
            }
            catch (Exception ex)
            {
                Logger?.Error($"Caught exception while stopping {ServiceName} : {ex}", ex);
            }
            finally
            {
                Logger?.Info($"{ServiceName} stopped.");
            }
        }
    }
}