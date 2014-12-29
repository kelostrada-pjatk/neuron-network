using System;

namespace NeuronNetwork.Activation
{
    public class ActivationSigmoid : IActivationFunction
    {
        public double ActivationFunction(double d)
        {
            return 1.0 / (1 + Math.Exp(-1.0 * d));
        }

        public double DerivativeFunction(double d)
        {
            return d * (1.0 - d);
        }
    }
}
