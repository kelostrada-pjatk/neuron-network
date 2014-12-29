using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuronNetwork.Neuron;

namespace NeuronNetwork
{
    public class NeuronLayer : ILayer
    {
        private int _neuronCount;

        public ILayer Next { get; set; }
        public ILayer Previous { get; set; }
        public List<INode> Nodes { get; private set; }


        public NeuronLayer(int neuronCount)
        {
            _neuronCount = neuronCount;
            Nodes = new List<INode>();
        }






        
    }
}
