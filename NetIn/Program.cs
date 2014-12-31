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

        private static List<Matrix[]> TestingSet = new List<Matrix[]>();
        
        private static void Main(string[] args)
        {
            int i = 0;

            for (i = 0; i < 50000; i++)
            {
                TestingSet.Add(RandomCorrectMatrix());
                TestingSet.Add(RandomMatrix());
            }

            for (double Mi = 0.5; Mi <= 10; Mi += 0.5)
            {
                for (double Eta = 0.1; Eta <= 1; Eta += 0.1)
                {
                    Perceptron.Eta = Eta;
                    ActivationSigmoid.Mi = Mi;

                    var network = new MatrixInputNetwork();

                    var last100Errors = new Queue<double>();

                    for (i = 0; i < 100000; i++)
                    {
                        network.CalculateOutput(TestingSet[i]);
                        network.BackProp();
                        var actual = network.CalculateOutput(TestingSet[i]);
                        var expected = network.Output.ExpectedOutput;
                        var error = Math.Abs(expected - actual);

                        if (last100Errors.Count == 100)
                        {
                            last100Errors.Dequeue();
                        }

                        last100Errors.Enqueue(error);

                        if (last100Errors.Count == 100 && last100Errors.Average() < 0.01) break;

                        //Console.WriteLine("Actual: {0:0.#####}, Expected: {1}", actual, expected);
                    }

                    Console.WriteLine("Error: {1:0.0000}. Mi: {2:0.00}, Eta: {3:0.00}. Took {0} iterations.", i, last100Errors.Average(), Mi, Eta);
                }
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
