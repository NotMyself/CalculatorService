using CalculatorService.Extensions;
using Xunit;

namespace CalculatorService.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void CanConvertIntegerStringToDouble()
        {
            Assert.Equal(1, "1".ToDouble());
        }

        [Fact]
        public void CanConvertFloatStringToDouble()
        {
            Assert.Equal(1.2, "1.2".ToDouble());
        }

        [Fact]
        public void ConvertingInvalidValueReturnsNull()
        {
            Assert.Null("foo".ToDouble());
        }
    }
}
