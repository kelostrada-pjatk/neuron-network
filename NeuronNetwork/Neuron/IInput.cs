using System.Collections.Generic;

namespace NeuronNetwork.Neuron
{
    public interface IInput
    {
        double Error { get; }
        List<IOutput> Inputs { get; }
        void AddInput(IOutput input);
    }
}
