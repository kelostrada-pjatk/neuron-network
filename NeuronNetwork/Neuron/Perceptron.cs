using System;
using System.Collections.Generic;
using System.Linq;
using NeuronNetwork.Activation;

namespace NeuronNetwork.Neuron
{
    public class Perceptron : INode
    {
        private static readonly Random Random = new Random();

        private bool _outputCalculated = false;
        private double _output;

        public double OutputValue
        {
            get
            {
                if (_outputCalculated) return _output;
                CalculateOutput();
                return _output;
            }
        }

        public double Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public List<IOutput> Inputs { get; private set; }
        public List<double> Weights { get; private set; }

        public List<IInput> Outputs { get; private set; }

        public Bias Bias { get; set; }
        public double BiasWeight { get; private set; }

        public IActivationFunction ActivationFunction { get; private set; }

        public Perceptron(IActivationFunction activationFunction, params IOutput[] inputs)
        {
            Inputs = new List<IOutput>();
            Weights = new List<double>();
            Outputs = new List<IInput>();
            Bias = new Bias(1);
            BiasWeight = Random.NextDouble()*2 - 1;
            ActivationFunction = activationFunction;
            foreach (var input in inputs)
            {
                AddInput(input);
            }
        }

        public void AddInput(IOutput input, double weight)
        {
            Inputs.Add(input);
            Weights.Add(weight);
            input.Outputs.Add(this);
        }

        public void AddInput(IOutput input)
        {
            AddInput(input, Random.NextDouble()*2 - 1);
        }
        
        private void CalculateOutput()
        {
            var output = Inputs.Select((t, i) => t.OutputValue*Weights[i]).Sum();
            output += Bias.OutputValue*BiasWeight;
            output /= Inputs.Count + 1;
            _output = ActivationFunction.ActivationFunction(output);
            _outputCalculated = true;
        }

    }
}
