using System.Runtime.Serialization;

namespace CalculatorService.Service
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