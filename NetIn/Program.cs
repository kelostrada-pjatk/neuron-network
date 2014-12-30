using NeuronNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuronNetwork.Activation;
using NeuronNetwork.MatrixInput;
using NeuronNetwork.Neuron;

namespace NetIn
{
    class Program
    {
        private static Matrix[] RandomMatrix()
        {
            var inputs = new Matrix[5];
            for (var i = 0; i < 5; i++) inputs[i] = Matrix.Random();
            return inputs;
        }

        private static Matrix[] RandomCorrectMatrix()
        {
            var inputs = new[]
            {
                Matrix.Random(1),
                Matrix.Random(1),
                Matrix.Random(0),
                Matrix.Random(2),
                Matrix.Random(7)
            };
            return inputs;
        }
        
        private static void Main(string[] args)
        {
            var network = new MatrixInputNetwork();

            for (int i = 0; i < 2000; i++)
            {
                network.CalculateOutput(RandomMatrix());
                network.BackProp();
                var actual = network.CalculateOutput(RandomMatrix());
                var expected = network.Output.ExpectedOutput;

                Console.WriteLine("Actual: {0}, Expected: {1}", actual, expected);

                network.CalculateOutput(RandomMatrix());
                network.BackProp();
                actual = network.CalculateOutput(RandomMatrix());
                expected = network.Output.ExpectedOutput;

                Console.WriteLine("Actual: {0}, Expected: {1}", actual, expected);

                network.CalculateOutput(RandomCorrectMatrix());
                network.BackProp();
                actual = network.CalculateOutput(RandomCorrectMatrix());
                expected = network.Output.ExpectedOutput;

                Console.WriteLine("Actual: {0}, Expected: {1}", actual, expected);
            }

        }




        #region Old network
        static void OldNetwork()
        {
            Input inp1 = new Input(2);
            Input inp2 = new Input(-1);

            Perceptron P3 = new Perceptron(new ActivationThreshold(3));
            P3.AddInput(inp1);
            P3.AddInput(inp2, 0);
            //P3.AddInput(inp1, 0);

            Perceptron P4 = new Perceptron(new ActivationThreshold(3));
            P4.AddInput(inp2, -1);
            P4.AddInput(inp2, 0);
            P4.AddInput(inp2, 0);

            Perceptron P5 = new Perceptron(new ActivationThreshold(0));
            P5.AddInput(inp2, -1);
            P5.AddInput(inp2, 1);
            P5.AddInput(inp2, 1);

            Perceptron P6 = new Perceptron(new ActivationThreshold(1));
            //P6.AddInput(inp2, 1);
            P6.AddInput(inp2, 0);
            P6.AddInput(inp2, 1);

            Perceptron P2 = new Perceptron(new ActivationThreshold(-3));
            P2.AddInput(P5, 1);
            P2.AddInput(P6, -1);
            P2.AddInput(P3, 0);

            Perceptron P1 = new Perceptron(new ActivationThreshold(-1));
            //P1.AddInput(P2, 1);
            P1.AddInput(P2, 1);
            P1.AddInput(inp1, -1);

            Console.WriteLine(P1.OutputValue);
        }
        #endregion

    }

}
