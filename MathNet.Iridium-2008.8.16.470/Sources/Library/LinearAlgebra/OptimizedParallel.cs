#region License

// 
// Author: Ian Davis <ian@innovatian.com>
// Copyright (c) 2010, Innovatian Software, LLC
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

#endregion

namespace MatrixMultiplication
{
    public class OptimizedParallel
    {
        private static int ChunkFactor { get; set; }

        public static double[][] Multiply(double[][] A, double[][] B)
        {
            return Multiply(A, B, Environment.ProcessorCount);
        }

        public static double[][] Multiply(double[][] A_, double[][] B_, int chunkFactor)
        {
            ChunkFactor = chunkFactor;
            int N = A_.Length;
            var C = new double[A_.Length][];

            int index = 0;
            int lin = C.Length;
            int col = B_[0].Length;
            do
            {

                C[index] = new double[col];
                index++;
            } while (index < lin);

            var A = A_;
            var B = B_;

            //Util.Initialize( N, A, B, C );

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            IEnumerable<Tuple<int, double[][]>> data = PartitionData(N, A);
            Parallel.ForEach(data, item => Multiply(item, B, C));
            stopwatch.Stop();
            return C;
        }

        private static IEnumerable<Tuple<int, double[][]>> PartitionData(int N, double[][] A)
        {
            int pieces = ((N % ChunkFactor) == 0)
                             ? N / ChunkFactor
                             : ((int)((N) / ((float)ChunkFactor)) + 1);

            int remaining = N;
            int currentRow = 0;

            while (remaining > 0)
            {
                if (remaining < ChunkFactor)
                {
                    ChunkFactor = remaining;
                }

                remaining = remaining - ChunkFactor;
                var ai = new double[ChunkFactor][];
                for (int i = 0; i < ChunkFactor; i++)
                {
                    ai[i] = A[currentRow + i];
                }

                int oldRow = currentRow;
                currentRow += ChunkFactor;
                yield return new Tuple<int, double[][]>(oldRow, ai);
            }
        }

        private static void Multiply(Tuple<int, double[][]> A, double[][] B, double[][] C)
        {
            int size = A.Item2.GetLength(0);
            int cols_b = B[0].Length;
            int cols_a = A.Item2[0].Length;
            double[][] ai = A.Item2;

            int i = 0;
            int offset = A.Item1;
            do
            {
                int k = 0;
                do
                {
                    int j = 0;
                    do
                    {
                        double[] ci = C[offset];
                        ci[j] = (ai[i][k] * B[k][j]) + ci[j];
                        j++;
                    } while (j < cols_b);
                    k++;
                } while (k < cols_a);
                i++;
                offset++;
            } while (i < size);
        }
    }
}