namespace CalculatorWorkflowService.Web
{
    using System.ServiceModel;

    [ServiceContract]
    public interface ICalculatorService
    {
        [OperationContract]
        int Multiply(int value1, int value2);
    }
}
