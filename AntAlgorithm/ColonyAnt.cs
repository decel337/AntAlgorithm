using System.Collections;
using System.Collections.Generic;

namespace AntAlgorithm
{
    public class ColonyAnt: IEnumerator,IEnumerable
    {
        public double[,] Pheromone { get; set; }
        public int[,] Distance { get; set; }
        public List<Ant> Ants = new List<Ant>();
        public int Lmin { get; set; }
        int position = -1;
        
        public ColonyAnt(int[,] distance, double[,] pheromone, int Lmin)
        {
            Pheromone = pheromone;
            Distance = distance;
            this.Lmin = Lmin;
            InitColony();
        }

        private void InitColony()
        {
            int place = 0;
            for (int i = 0; i < 15; i++)
            {
                Ants.Add(new DefoltAnt(place, Pheromone, Distance));
                place += 4;
                Ants.Add(new DefoltAnt(place, Pheromone, Distance));
                place += 4;
                Ants.Add(new WildAnt(place, Pheromone, Distance));
                place += 4;
            }
        }
        
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }
        
        public bool MoveNext()
        {
            position++;
            return (position < Ants.Count);
        }

        public void Reset()
        {
            position = -1;
        }

        public object Current
        {
            get { return Ants[position];}
        }
        
        public void ChangePheromon()
        {
            for (int i = 0; i < Pheromone.GetLength(0); i++)
            {
                for (int j = i+1; j < Pheromone.GetLength(1); j++)
                {
                    Pheromone[i, j] *= 0.7;
                    Pheromone[j, i] *= 0.7;
                }
            }

            foreach (var ant in Ants)
            {
                for (int i = 0; i < ant.TraceWay.Count-1; i++)
                {
                    int startLocal = ant.TraceWay[i];
                    int nextLocal = ant.TraceWay[i+1];

                    Pheromone[startLocal, nextLocal] += (double)ant.LengthWay / Lmin;
                    Pheromone[nextLocal, startLocal] += (double)ant.LengthWay / Lmin;
                }
            }
        }

        public void ResetAnts()
        {
            Ants.Clear();
            InitColony();
            position = -1;
        }
    }
}