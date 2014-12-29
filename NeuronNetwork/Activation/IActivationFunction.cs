namespace NeuronNetwork.Activation
{
    public interface IActivationFunction
    {
        double ActivationFunction(double d);
        double DerivativeFunction(double d);
    }
}
