using System.Runtime.Serialization;

namespace CalculatorService
{
    [DataContract]
    public class CalculationResult
    {
        [DataMember]
        public double Answer;

        [DataMember]
        public string Message;
    }
}