using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorWorkflowService.Tests
{
    [TestClass]
    public class MultiplyTests
    {
        [TestMethod]
        public void MultiplyTest()
        {
            var num1 = 2;
            var num2 = 5;
            var expectedResult = 10;
            int actualResult;

            using (var multiplyClient = new MultiplyWorkflowService.ServiceClient())
            {
                actualResult = multiplyClient.Multiply(num1, num2);
            }

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
