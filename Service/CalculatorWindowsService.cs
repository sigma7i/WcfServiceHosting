using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ServiceModel.Samples
{
    public class CalculatorWindowsService : ServiceBase
    {
        public ServiceHost serviceHost = null;

        public CalculatorWindowsService()
        {
            // Name the Windows Service
            ServiceName = "WCFWindowsServiceSample";
        }

        /// <summary>
        /// из примера https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/how-to-host-a-wcf-service-in-a-managed-windows-service#Y382
        /// </summary>
        public static void Main()
        {
            ServiceBase.Run(new CalculatorWindowsService());
        }

        // Start the Windows service.
        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            // Create a ServiceHost for the CalculatorService type and 
            // provide the base address.
            serviceHost = new ServiceHost(typeof(CalculatorService));

            // Open the ServiceHostBase to create listeners and start 
            // listening for messages.
            serviceHost.Open();
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
