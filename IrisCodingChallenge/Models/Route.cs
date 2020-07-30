using System.Collections.Generic;
using System.Linq;

namespace IrisCodingChallenge.Models
{
    public class Route
    {
        public List<Vertex> Verticies { get; } = new List<Vertex>();

        public string[] ToDirections()
        {
            List<string> directions = new List<string>();

            Vertex previousVertex = Verticies.First();
            foreach (Vertex routeVertex in Verticies.Skip(1))
            {
                directions.Add(previousVertex.DirectionTo(routeVertex).ToString());
                previousVertex = routeVertex;
            }

            return directions.ToArray();
        }
    }
}
