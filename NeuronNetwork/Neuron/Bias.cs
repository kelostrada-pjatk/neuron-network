using System.Collections.Generic;

namespace NeuronNetwork.Neuron
{
    public class Bias : IOutput
    {
        public Bias(double biasValue)
        {
            OutputValue = biasValue;
            Outputs = new List<IInput>();
        }

        public double OutputValue { get; private set; }


        public System.Collections.Generic.List<IInput> Outputs { get; private set; }
    }
}
