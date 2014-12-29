using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronNetwork.Activation
{
    public class ActivationThreshold : IActivationFunction
    {
        private readonly double _threshold;

        public ActivationThreshold(double threshold)
        {
            _threshold = threshold;
        }

        public double ActivationFunction(double d)
        {
            return d >= _threshold ? 1 : 0;
        }

        public double DerivativeFunction(double d)
        {
            throw new NotImplementedException();
        }
    }
}
