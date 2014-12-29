using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NeuronNetwork.MatrixInput
{
    public class Matrix
    {
        public const int Size = 3;

        private readonly double[,] _values;

        public double this[int i, int j]
        {
            get { return _values[i, j]; }
            set { _values[i, j] = value; }
        }

        public double Value
        {
            get { return _values.Cast<double>().Sum(); }
        }

        public Matrix(double[,] values)
        {
            if (values.GetLength(0) != Size || values.GetLength(1) != Size)
            {
                throw new ArgumentException("Invalid array size");
            }

            _values = values;
        }

        private static readonly Random Rand = new Random();

        public static Matrix Random()
        {
            var values = new double[Size,Size];

            var val = Rand.Next(0, 10);

            while (val > 0)
            {
                var i = Rand.Next(0, 3);
                var j = Rand.Next(0, 3);

                if (values[i, j].CompareTo(0) == 0)
                {
                    values[i, j] = 1;
                    val--;
                }
            }

            return new Matrix(values);
        }

        public static Matrix Concrete(int value)
        {
            var values = new double[Size, Size];
            for (var i = 0; i < value; i++)
            {
                values[i/Size, i%Size] = 1;
            }
            return new Matrix(values);
        }

    }
}
