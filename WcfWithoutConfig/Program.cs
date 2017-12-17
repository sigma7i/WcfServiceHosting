using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using Topshelf;

namespace WcfWithoutConfig
{
    class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<TopshelfService>();

                x.RunAsLocalSystem();
                x.SetDescription("WcfWithoutConfig Example");
                x.SetDisplayName("WcfWithoutConfig Example");
                x.SetServiceName("WcfWithoutConfig");
            });
        }
    }

    class TopshelfService : ServiceControl
    {
        private ServiceHost serviceHost = null;

        public bool Start(HostControl hostControl)
        {
            serviceHost?.Close();

            //Для создания и запуска WCF-службы не обязательно создавать конфигурационный файл для приложения
            //Это имеет смысл в службах, настройки которых никогда не будут меняться
            //Данный метод не является лучшей практикой использования WCF. 
            //Для создания расширяемых и модульных приложений лучше использовать стандартный способ: комбинация app.config и запуска сервиса из кода.


            //Чтобы WCF-сервис заработал достаточно написать следующие строки:
            ServiceHost host = new ServiceHost(typeof(MyService));

            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxReceivedMessageSize = Int32.MaxValue;
            binding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;

            host.AddServiceEndpoint(typeof(IMyService), binding, new Uri("http://127.0.0.1:12345/"));

            //нужно добавить "поведение" нашему сервису и указать точку подключения, через которую можно будет скачать WSDL. Делается это так:
            host.Description.Behaviors.Add(new ServiceMetadataBehavior());
            host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
            MetadataExchangeBindings.CreateMexHttpBinding(), "http://127.0.0.1:12345/mex");

            host.Open();

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
