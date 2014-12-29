using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuronNetwork.Neuron;

namespace NeuronNetwork
{
    public interface ILayer
    {
        ILayer Next { get; set; }
        ILayer Previous { get; set; }
        List<INode> Nodes { get; }
    }
}
