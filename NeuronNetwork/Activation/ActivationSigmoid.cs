using System;

namespace NeuronNetwork.Activation
{
    public class ActivationSigmoid : IActivationFunction
    {
        public static double Mi = 1.0;

        public double ActivationFunction(double d)
        {
            return 1.0 / (1 + Math.Exp(-Mi * d));
        }

        public double DerivativeFunction(double d)
        {
            return d * (1.0 - d) * Mi;
        }
    }
}
