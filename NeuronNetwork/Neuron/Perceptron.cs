using System;
using System.Collections.Generic;
using System.Linq;
using NeuronNetwork.Activation;

namespace NeuronNetwork.Neuron
{
    public class Perceptron : INode
    {
        private static readonly Random Random = new Random();

        public void ResetOutput()
        {
            _outputCalculated = false;
            _errorCalculated = false;
        }

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

        private bool _isOutput = false;

        public double ExpectedOutput { get; set; }

        public double GetWeight(IOutput input)
        {
            int index = Inputs.FindIndex(i => i == input);
            return Weights[index];
        }

        private bool _errorCalculated = false;
        private double _error;

        public double Error
        {
            get
            {
                if (_errorCalculated)
                {
                    return _error;
                }

                double d = 0;
                if (_isOutput)
                {
                    d = (ExpectedOutput - _output);
                }
                else
                {
                    foreach (var o in Outputs)
                    {
                        d += o.Error * o.GetWeight(this);
                    }
                }
                _error = ActivationFunction.DerivativeFunction( _output ) * d;
                _errorCalculated = true;
                return _error;
            }
        }

        public static double Eta = 0.1;

        public void CorrectWeights()
        {
            for (var i = 0; i < Inputs.Count; i++)
            {
                Weights[i] += Eta * Error * Inputs[i].OutputValue;
            }
            BiasWeight += Eta * Error * Bias.OutputValue;
        }

        public List<IOutput> Inputs { get; private set; }
        public List<double> Weights { get; private set; }

        public List<IInput> Outputs { get; private set; }

        public Bias Bias { get; set; }
        public double BiasWeight { get; set; }

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

        public Perceptron(IActivationFunction activationFunction, bool isOutput)
            : this(activationFunction)
        {
            _isOutput = isOutput;
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
