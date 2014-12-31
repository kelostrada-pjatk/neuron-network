using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuronNetwork.Activation;
using NeuronNetwork.Neuron;

namespace NeuronNetwork.MatrixInput
{
    public class MatrixInputNetwork
    {
        private const int Size = 5;
        private static readonly double[] Expected = { 1, 1, 0, 2, 7 };
        private bool _expectedResult = true;

        private readonly List<Input> _inputs = new List<Input>();
        private readonly List<Perceptron> _layer1 = new List<Perceptron>();
        private readonly List<Perceptron> _layer2 = new List<Perceptron>();

        public Perceptron Output { get; private set; }

        public MatrixInputNetwork()
        {
            // Create output
            Output = new Perceptron(new ActivationSigmoid(), true);

            // Create layer1 and pin inputs to it
            for (var k = 0; k < Size; k++)
            {
                var perceptron = new Perceptron(new ActivationSigmoid());
                for (var i = 0; i < Matrix.Size; i++)
                {
                    for (var j = 0; j < Matrix.Size; j++)
                    {
                        var input = new Input(0);
                        perceptron.AddInput(input);
                        _inputs.Add(input);
                    }
                }
                _layer1.Add(perceptron);
            }

            // Create layer2 and pin it to output and to layer1
            for (var i = 0; i < Size; i++)
            {
                var perceptron = new Perceptron(new ActivationSigmoid());
                foreach (var p in _layer1)
                {
                    perceptron.AddInput(p);
                }

                _layer2.Add(perceptron);
                Output.AddInput(perceptron);
            }

        }

        public double CalculateOutput(Matrix[] inputs)
        {
            ResetOutputs();

            // Need to check expected result
            _expectedResult = true;
            for (var i = 0; i < Size; i++)
            {
                _expectedResult &= Expected[i].CompareTo(inputs[i].Value) == 0;
            }

            Output.ExpectedOutput = _expectedResult ? 1 : 0;

            for (var k = 0; k < Size; k++)
            {
                for (var i = 0; i < Matrix.Size; i++)
                {
                    for (var j = 0; j < Matrix.Size; j++)
                    {
                        var index = k * Matrix.Size * Matrix.Size + i * Matrix.Size + j;
                        _inputs[index].OutputValue = inputs[k][i, j];
                    }
                }
            }

            return Output.OutputValue;
        }

        public void ResetOutputs()
        {
            foreach (var p in _layer1)
            {
                p.ResetOutput();
            }

            foreach (var p in _layer2)
            {
                p.ResetOutput();
            }

            Output.ResetOutput();
        }

        public void BackProp()
        {
            Output.CorrectWeights();

            foreach (var p in _layer2)
            {
                p.CorrectWeights();
            }

            foreach (var p in _layer1)
            {
                p.CorrectWeights();
            }
        }

        public double[, ,] GetNetworkWeights()
        {
            var inputsCount = Matrix.Size * Matrix.Size;

            double[, ,] weights = new double[3, Size, inputsCount + 1];

            // Fill weights from layer1
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < inputsCount; j++)
                {
                    weights[0, i, j] = _layer1[i].Weights[j];
                }
                weights[0, i, inputsCount] = _layer1[i].BiasWeight;
            }

            // Fill weights from layer2
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    weights[1, i, j] = _layer2[i].Weights[j];
                }
                weights[1, i, Size] = _layer2[i].BiasWeight;
            }

            // Fill weights from output layer
            for (int i = 0; i < Size; i++)
            {
                weights[2, 0, i] = Output.Weights[i];
            }
            weights[2, 0, Size] = Output.BiasWeight;

            return weights;
        }

        public void SetNetworkWeights(double[, ,] weights)
        {
            var inputsCount = Matrix.Size * Matrix.Size;

            // Fill weights of layer1
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < inputsCount; j++)
                {
                    _layer1[i].Weights[j] = weights[0, i, j];
                }
                _layer1[i].BiasWeight = weights[0, i, inputsCount];
            }

            // Fill weights of layer2
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    _layer2[i].Weights[j] = weights[1, i, j];
                }
                _layer2[i].BiasWeight = weights[1, i, Size];
            }

            // Fill weights of output layer
            for (int i = 0; i < Size; i++)
            {
                Output.Weights[i] = weights[2, 0, i];
            }
            Output.BiasWeight = weights[2, 0, Size];

        }
    }
}
