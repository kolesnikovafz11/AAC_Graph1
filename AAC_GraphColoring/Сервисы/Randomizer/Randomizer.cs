using System;
using System.Linq;
using System.Security.Cryptography;

namespace AAC_Graph.Сервисы.Randomizer
{
    public static class Randomizer
    {
        public static byte[,] GetRandomAdjacencyMatrix(int dim)
        {
            var matrix = new byte[dim, dim];
            
            var randNormal = new RNGCryptoServiceProvider();
            for (var i = 0; i < dim; i++)
            {
                for (var j = 0; j <= i; j++)
                {
                    
                    if (i == j)
                    {
                        matrix[i, j] = 0;
                    }
                    else
                    {
                        var arr = new byte[1];
                        randNormal.GetBytes(arr);
                        var state = arr.First();
                        var isAdjacent = state % 2 == 0;
                        if (isAdjacent)
                        {
                            matrix[i, j] = 0;
                        }
                        else
                        {
                            matrix[i, j] = 1;
                        }
                        matrix[j, i] = matrix[i, j];
                    }
                    
                }
            }

            return matrix;
        }
    }
}