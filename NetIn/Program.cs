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

        private static void Main(string[] args)
        {
            var inputs = new Matrix[5];

            for (var i = 0; i < 5; i++)
            {
                inputs[i] = Matrix.Random();
            }

            var network = new MatrixInputNetwork();

            Console.WriteLine(network.CalculateOutput(inputs));

            inputs = new[]
            {
                Matrix.Concrete(1),
                Matrix.Concrete(1),
                Matrix.Concrete(0),
                Matrix.Concrete(2),
                Matrix.Concrete(7)
            };

            Console.WriteLine(network.CalculateOutput(inputs));
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
