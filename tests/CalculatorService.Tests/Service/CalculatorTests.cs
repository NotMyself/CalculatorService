using CalculatorService.Service;
using Xunit;

namespace CalculatorService.Tests.Service
{
    public class CalculatorTests
    {
        [Fact]
        public void CanAddNumbers()
        {
            ICalculator calculator = new Calculator();
            var result = calculator.Add("1", "1");

            Assert.Equal(2, result.Answer);
        }

        [Fact]
        public void CanSubtractNumbers()
        {
            ICalculator calculator = new Calculator();
            var result = calculator.Subtract("1", "1");

            Assert.Equal(0, result.Answer);
        }

        [Fact]
        public void CanMultiplyNumbers()
        {
            ICalculator calculator = new Calculator();
            var result = calculator.Multiply("5", "5");

            Assert.Equal(25, result.Answer);
        }

        [Fact]
        public void CanDivideNumbers()
        {
            ICalculator calculator = new Calculator();
            var result = calculator.Divide("25", "5");

            Assert.Equal(5, result.Answer);
        }
    }
}
