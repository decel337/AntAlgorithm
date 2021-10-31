using System;
using System.Runtime.InteropServices;

namespace AntAlgorithm
{
    public class DefoltAnt:Ant
    {
        const int alpha = 2;
        const int beta = 4;
        public DefoltAnt(int place, double[,] pheromone, int[,] distance) : base(place, pheromone, distance)
        {
            
        }
        public override void Run()
        {
            int startlocal = startPoint;
            Random rand = new Random();
            for (int i = 0; i < Distance.GetLength(0); i++)
            {
                double totalPercent = 0;
                double currentPercent = 0;
                int nextPoint = 0;

                foreach (var vert in AvailableVert)
                {
                    totalPercent += Math.Pow(Pheromone[startlocal, vert], alpha)*Math.Pow((double)1/Distance[startlocal, vert], beta);
                }

                double randomPercent = rand.NextDouble();

                foreach (var vert in AvailableVert)
                {
                    currentPercent += Math.Pow(Pheromone[startlocal, vert], alpha) *
                                      Math.Pow((double) 1 / Distance[startlocal, vert], beta)/totalPercent;
                    if (currentPercent > randomPercent)
                    {
                        nextPoint = vert;
                        break;
                    }
                }

                LengthWay += Distance[startlocal, nextPoint];
                
                AvailableVert.Remove(nextPoint);
                TraceWay.Add(nextPoint);
                startlocal = nextPoint;
            }
            TraceWay.Add(startPoint);
            LengthWay += Distance[startlocal, startPoint];
        }
    }
}