using System.ServiceModel;

namespace WcfNamedPipes
{
	// Define a service contract.
	[ServiceContract]
	public interface ICalculator
	{
		[OperationContract]
		double Add(double n1, double n2);
		[OperationContract]
		double Subtract(double n1, double n2);
		[OperationContract]
		double Multiply(double n1, double n2);
		[OperationContract]
		double Divide(double n1, double n2);

		[OperationContract]
		void DoStackOverflow();

		[OperationContract]
		void DoAnyExeption();
	}
}
