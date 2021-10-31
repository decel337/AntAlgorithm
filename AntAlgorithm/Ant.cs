using System;
using System.Collections.Generic;
using System.Linq;

namespace AntAlgorithm
{
    public class Ant
    {
        public int startPoint { get; set; }
        public double[,] Pheromone { get; set; }
        public int[,] Distance { get; set; }
        public List<int> TraceWay { get; set; }
        public int LengthWay = 0;
        public List<int> AvailableVert { get; set; }

        public Ant(int startPoint, double[,] pheromone, int[,] distance)
        {
            TraceWay = new List<int>(startPoint);
            this.startPoint = startPoint;
            AvailableVert = Enumerable.Range(0, 200).ToList();
            AvailableVert.Remove(startPoint);
            Pheromone = pheromone;
            Distance = distance;
        }
        public virtual void Run()
        {
            Console.WriteLine("Unknown ant");
        }
    }
}