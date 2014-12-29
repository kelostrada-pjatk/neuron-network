using System.Collections.Generic;

namespace NeuronNetwork.Neuron
{
    public class Input : IOutput
    {
        public Input(double inputValue)
        {
            OutputValue = inputValue;
            Outputs = new List<IInput>();
        }

        public double OutputValue { get; set; }


        public List<IInput> Outputs { get; private set; }
    }
}
