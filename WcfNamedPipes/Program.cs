using System;
using System.ServiceModel;
using Topshelf;

namespace WcfNamedPipes
{
	class Program
	{
		private const string ServiceName = "WcfNamedPipesService";
		public static void Main(string[] args)
		{
			HostFactory.Run(x =>
			{
				x.Service<TopshelfService>();

				x.StartAutomatically();
				x.RunAsLocalSystem();
				x.SetDescription("WcfNamedPipes Service Example");
				x.SetDisplayName("WcfNamedPipesService Example");
				x.SetServiceName(ServiceName);
				x.SetStartTimeout(TimeSpan.FromSeconds(30));
				x.SetStopTimeout(TimeSpan.FromSeconds(60));
			});
		}
	}



	class TopshelfService : ServiceControl
	{
		private ServiceHost serviceHost = null;

		public bool Start(HostControl hostControl)
		{
			serviceHost?.Close();

			serviceHost = new ServiceHost(typeof(CalculatorService));

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
