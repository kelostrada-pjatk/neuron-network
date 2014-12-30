using System.Collections.Generic;

namespace NeuronNetwork.Neuron
{
    public interface IInput
    {
        double Error { get; }
        double GetWeight(IOutput input);
        List<IOutput> Inputs { get; }
        void AddInput(IOutput input);
    }
}
