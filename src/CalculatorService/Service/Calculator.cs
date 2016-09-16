using System;
using System.ServiceModel;
using CalculatorService.Extensions;
using NLog;

namespace CalculatorService.Service
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = false,
         AddressFilterMode = AddressFilterMode.Any,
         InstanceContextMode = InstanceContextMode.Single,
         ConcurrencyMode = ConcurrencyMode.Single)]
    public class Calculator : ICalculator
    {
        public Calculator()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }

        public ILogger Logger { get; set; }

        public CalculationResult Add(string n1, string n2)
        {
            Logger?.Info($"received request to Add {n1} to {n2}");
           
            return Calculate(n1, n2, _add);
        }

        public CalculationResult Subtract(string n1, string n2)
        {
            Logger?.Info($"received request to Subtract {n2} from {n1}");
            
            return Calculate(n1, n2, _subtract);
        }

        public CalculationResult Multiply(string n1, string n2)
        {
            Logger?.Info($"received request to Multiply {n1} by {n2}");
            
            return Calculate(n1, n2, _multiply);
        }

        public CalculationResult Divide(string n1, string n2)
        {
            Logger?.Info($"received request to Divide {n1} by {n2}");
           
            return Calculate(n1, n2, _divide);
        }

        private static CalculationResult Calculate(string n1, string n2, Func<double, double, double> calculate)
        {
            var value1 = n1.ToDouble();
            if (!value1.HasValue)
            {
                return GetCouldNotConvertToDoubleResult(n1);
            }

            var value2 = n2.ToDouble();
            if (!value2.HasValue)
            {
                return GetCouldNotConvertToDoubleResult(n2);
            }

            double result = calculate(value1.Value, value2.Value);
            return new CalculationResult
            {
                Answer = result
            };
        }

        private static readonly Func<double, double, double> _add = (value1, value2) => value1 + value2;
        private static readonly Func<double, double, double> _subtract = (value1, value2) => value1 - value2;
        private static readonly Func<double, double, double> _multiply = (value1, value2) => value1 * value2;
        private static readonly Func<double, double, double> _divide = (value1, value2) => value2 == 0 ? double.NaN : value1 / value2;
        
        private static CalculationResult GetCouldNotConvertToDoubleResult(string input)
        {
            return new CalculationResult
            {
                Message = $"Could not convert '{input}' to a double"
            };
        }
    }
}