using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronNetwork
{
    public class Network
    {






        public List<ILayer> Layers { get; private set; }

        public Network()
        {
            Layers = new List<ILayer>();
        }

        public void AddLayer(ILayer layer)
        {
            Layers.Add(layer);
        }




    }
}
