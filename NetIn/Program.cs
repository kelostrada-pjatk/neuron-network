using NeuronNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuronNetwork.Activation;
using NeuronNetwork.MatrixInput;
using NeuronNetwork.Neuron;
using System.IO;

namespace NetIn
{
    class Program
    {
        
        private static void Main(string[] args)
        {
            var network = new MatrixInputNetwork();
            var randomedWeights = network.GetNetworkWeights();

            StreamWriter file = new StreamWriter("output_data6.csv", true);

            int i = 0;

            for (i = 0; i < 50000; i++)
            {
                TestingSet.Add(RandomCorrectMatrix());
                TestingSet.Add(RandomMatrix());
            }

            for (double Eta = 0.025; Eta <= 1.025; Eta += 0.025)
            {
                file.Write(";{0:0.000}", Eta);
            }

            file.WriteLine();
            file.Close();

            for (double Mi = 0.25; Mi <= 30; Mi += 0.25)
            {
                file = new StreamWriter("output_data6.csv", true);

                file.Write("{0:0.00};", Mi);

                for (double Eta = 0.025; Eta <= 1.025; Eta += 0.025)
                {
                    Perceptron.Eta = Eta;
                    ActivationSigmoid.Mi = Mi;

                    network.SetNetworkWeights(randomedWeights);

                    var last100Errors = new Queue<double>();

                    for (i = 0; i < 3000; i++)
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

                        if (last100Errors.Count == 100 && last100Errors.Average() < 0.1) break;
                    }

                    Console.WriteLine("Error: {1:0.0000}. Mi: {2:0.00}, Eta: {3:0.000}. Took {0} iterations.", i, last100Errors.Average(), Mi, Eta);
                    //file.WriteLine("{0:0.00};{1:0.000};{3}", Mi, Eta, last100Errors.Average(), i);
                    file.Write("{0};", i);
                }

                file.WriteLine();

                file.Close();
            }

            
        }

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

        
    }

}
