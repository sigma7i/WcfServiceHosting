﻿using System;
using System.IO;
using System.ServiceModel;
using Topshelf;

namespace ServiceTopshelf
{
    public class TopshelfInstaller
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<TopshelfService>();

                x.StartAutomatically();
                x.RunAsLocalSystem();
                x.SetDescription("WсfTopshelf Service Example");
                x.SetDisplayName("WсfTopshelfService Example");
                x.SetServiceName("WсfTopshelfService");
                x.EnableServiceRecovery(rc => { rc.RestartService(0);
                                                rc.RestartService(0);
                                                rc.RestartService(0);
                });
                x.OnException(ExeptionHandling);
            });
        }

        private static void ExeptionHandling(Exception ex)
        {
            // do something
        }
    }



    class TopshelfService : ServiceControl
    {
        private ServiceHost serviceHost = null;

        public bool Start(HostControl hostControl)
        {
            serviceHost?.Close();

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
