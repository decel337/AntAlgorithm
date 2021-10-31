using System;
using System.Collections.Generic;

namespace AntAlgorithm
{
    public class WildAnt:Ant
    {
        public WildAnt(int place, double[,] pheromone, int[,] distance) : base(place, pheromone, distance)
            {
                
            }

        public override void Run()
        {
            int startlocal = startPoint;
            Random rand = new Random();
            for (int i = 0; i < Distance.GetLength(0)-1; i++)
            {
                int nextPointindex = rand.Next(0, AvailableVert.Count);
                int nextPoint = AvailableVert[nextPointindex];

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