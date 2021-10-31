using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AntAlgorithm
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            const int size = 200;
            int[,] distance = new int[size, size];
            double[,] pheromone = new double[size, size];
            InitMatrixes(distance, pheromone);
            const double dryingFactor = 0.7;
            int Lmin = GreedySerach(distance);
            int L = int.MaxValue;
            int winIter = 0;

            ColonyAnt colony = new ColonyAnt(distance, pheromone, Lmin);

            int numberOfIteration = 1000;
            
            for (int i = 0; i < numberOfIteration; i++)
            {
                foreach (Ant ant in colony)
                {
                   ant.Run();
                }

                List<Ant> test = colony.Ants.OrderBy(x => x.LengthWay).ToList();
                int MaybeL = colony.Ants.OrderBy(x => x.LengthWay).First().LengthWay;

                if (L > MaybeL)
                {
                    L = MaybeL;
                    winIter = i;
                }
                
                colony.ChangePheromon();
                colony.ResetAnts();
            }
            Console.WriteLine($"Iteration is {winIter} of {numberOfIteration} iterations ");
            Console.WriteLine($"Minimum way is  {L}");
        }

        private static int GreedySerach(int[,] distance)
        {
            int Lmin = int.MaxValue;

            for (int i = 0; i < distance.GetLength(0); i++)
            {
                int L = 0;
                List<int> OpenList = new List<int>();

                for (int j = 0; j < distance.GetLength(0); j++)
                {
                    OpenList.Add(j);
                }

                int startPoint = i;
                int currentL = int.MaxValue;
                OpenList.Remove(startPoint);

                while(OpenList.Any())
                {
                    int nextPoint = 0;

                    foreach (var vert in OpenList)
                    {
                        if (currentL > distance[startPoint, vert])
                        {
                            currentL = distance[startPoint, vert];
                            nextPoint = vert;
                        }
                    }
                    
                    L += currentL;
                    startPoint = nextPoint;
                    currentL = int.MaxValue;
                    OpenList.Remove(startPoint);
                }
                if (Lmin > L)
                {
                    Lmin = L;
                }
            }
            return Lmin;
        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i,j], 3}");
                }
                Console.WriteLine();
            }
        }

        private static void InitMatrixes(int[,] distance, double[,] pheromone)
        {
            Random rand = new Random();
            for (int i = 0; i < distance.GetLength(0); i++)
            {
                for (int j = i+1; j < distance.GetLength(1); j++)
                {
                    int dist = rand.Next(1, 40);
                    distance[i, j] = dist;
                    distance[j, i] = dist;
                    pheromone[i, j] = 1;
                    pheromone[j, i] = 1;
                }
            }
        }
    }
}