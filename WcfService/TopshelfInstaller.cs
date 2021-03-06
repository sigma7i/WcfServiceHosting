﻿using System.ServiceModel;
using Microsoft.ServiceModel.Samples;
using Topshelf;

namespace Service
{
    public class TopshelfInstaller
    {

        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<TopshelfService>();
                x.RunAsLocalSystem();
                x.SetDescription("Topshelf Service Example");
                x.SetDisplayName("TopshelfService Example");
                x.SetServiceName("TopshelfService");
            });
        }
    }

    class TopshelfService : ServiceControl
    {
        public ServiceHost serviceHost = null;

        public bool Start(HostControl hostControl)
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
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
            return true;
        }
    }
}
