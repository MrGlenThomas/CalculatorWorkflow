namespace CalculatorWorkflowService.Web
{
    public class CalculatorService : ICalculatorService
    {
        public int Multiply(int value1, int value2)
        {
            return value1 * value2;
        }
    }
}
