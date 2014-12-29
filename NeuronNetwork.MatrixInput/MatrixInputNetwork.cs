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
        private static readonly double[] Expected = {1, 1, 0, 2, 7};
        private bool _expectedResult = true;


        private readonly List<Input> _inputs = new List<Input>();
        private readonly List<Perceptron> _layer1 = new List<Perceptron>();
        private readonly List<Perceptron> _layer2 = new List<Perceptron>();

        public Perceptron Output { get; private set; }

        public MatrixInputNetwork()
        {
            // Create output
            Output = new Perceptron(new ActivationSigmoid());

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
            // Need to check expected result
            _expectedResult = true;
            for (var i = 0; i < Size; i++)
            {
                _expectedResult &= Expected[i].CompareTo(inputs[i].Value) == 0;
            }

            for (var k = 0; k < Size; k++)
            {
                for (var i = 0; i < Matrix.Size; i++)
                {
                    for (var j = 0; j < Matrix.Size; j++)
                    {
                        var index = k*Matrix.Size*Matrix.Size + i*Matrix.Size + j;
                        _inputs[index].OutputValue = inputs[k][i, j];
                    }
                }
            }

            return Output.OutputValue;
        }


    }
}
