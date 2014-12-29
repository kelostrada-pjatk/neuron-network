using System.Collections.Generic;

namespace NeuronNetwork.Neuron
{
    public interface IOutput
    {
        double OutputValue { get; }
        List<IInput> Outputs { get; }
    }
}
