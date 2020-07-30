using System;
using System.Collections.Generic;

namespace IrisCodingChallenge.Models
{
    public class GraphWorldMap : WorldGeometry
    {
        private readonly Vertex[,] verticies;

        public int MinElevation { get; private set; }
        public int MaxElevation { get; private set; }

        public int XMax => verticies.GetUpperBound(0);
        public int YMax => verticies.GetUpperBound(1);

        public HashSet<Vertex> AllVerticies { get; } = new HashSet<Vertex>();

        public GraphWorldMap(int maxX, int maxY, int minElevation, int maxElevation)
        {
            MinElevation = minElevation;
            MaxElevation = maxElevation;

            verticies = new Vertex[maxX, maxY];
            GenerateElevations(minElevation, maxElevation);
            ConnectAdjacentVerticies();
        }

        private void GenerateElevations(int minElevation, int maxElevation)
        {
            Random randomizer = new Random();

            int maxX = XMax;
            int maxY = YMax;

            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    int elevation = randomizer.Next(minElevation, maxElevation);
                    Vertex vertex = new Vertex(x, y, elevation);

                    verticies[x, y] = vertex;
                    AllVerticies.Add(vertex);
                }
            }
        }

        private void ConnectAdjacentVerticies()
        {
            int maxX = XMax;
            int maxY = YMax;

            // connect north-south edges
            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY - 1; y++)
                {
                    verticies[x, y].Connect(verticies[x, y + 1]);
                }
            }

            // connect east-west edges 
            for (int x = 0; x <= maxX - 1; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    verticies[x, y].Connect(verticies[x + 1, y]);
                }
            }
        }

        public Vertex GetVertex(int x, int y)
        {
            if (x >= 0 && y >= 0 && XMax >= x && YMax >= y && verticies[x, y] != null)
            {
                return verticies[x, y];
            }
            else
            {
                return null;
            }
        }

        public int GetElevation(int x, int y)
        {
            if (x >= 0 && y >= 0 && XMax >= x && YMax >= y && verticies[x, y] != null)
            {
                return verticies[x, y].Value;
            }
            else
            {
                return 0;
            }
        }
    }
}
