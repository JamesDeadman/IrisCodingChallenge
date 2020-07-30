using System.Collections.Generic;

namespace IrisCodingChallenge.Models
{
    public class Vertex
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public int Value { get; private set; }
        public HashSet<Edge> Edges { get; } = new HashSet<Edge>();

        public HashSet<Edge> InboundEdges { get; } = new HashSet<Edge>();
        public HashSet<Edge> OutboundEdges { get; } = new HashSet<Edge>();

        public Vertex(int x, int y, int value)
        {
            X = x;
            Y = y;
            Value = value;
        }

        /// <summary>
        /// Connect this vertex to another via an Edge
        /// </summary>
        /// <param name="vertex">The vertex to connect to</param>
        public void Connect(Vertex vertex)
        {
            int inboundCost = GetTravelCost(vertex);
            Edge inboundEdge = new Edge(vertex, this, inboundCost);

            int outboundCost = vertex.GetTravelCost(this);
            Edge outboundEdge = new Edge(this, vertex, outboundCost);

            Edges.Add(outboundEdge);
            Edges.Add(inboundEdge);

            OutboundEdges.Add(outboundEdge);
            InboundEdges.Add(inboundEdge);

            vertex.InboundEdges.Add(outboundEdge);
            vertex.OutboundEdges.Add(inboundEdge);

            vertex.Edges.Add(inboundEdge);
            vertex.Edges.Add(outboundEdge);
        }

        /// <summary>
        /// Find the direction from this vertex to a destination vertex
        /// </summary>
        /// <param name="vertex">The destination vertex</param>
        /// <returns>The direction as a Direction enum</returns>
        public Direction DirectionTo(Vertex vertex)
        {
            if(Y == vertex.Y && X == vertex.X)
            {
                return Direction.None;
            }
            else if(Y < vertex.Y && X == vertex.X)
            {
                return Direction.South;
            }
            else if(Y > vertex.Y && X == vertex.X)
            {
                return Direction.North;
            }
            else if(X < vertex.X && Y == vertex.Y)
            {
                return Direction.East;
            }
            else if(X > vertex.X && Y == vertex.Y)
            {
                return Direction.North;
            }
            else
            {
                return Direction.Unsupported;
            }
        }

        /// <summary>
        /// The cost of a single step is a function of elevation change
        /// </summary>
        /// <param name="destination">The vertex of the next step</param>
        /// <returns>The step cost as an integer</returns>
        public int GetTravelCost(Vertex destination)
        {
            int delta = destination.Value - Value;

            if(delta < -5) // Rappelling down a cliff is costly
            {
                return -delta;
            }
            else if(delta < 0) // Walking downhill is easy
            {
                return 5 + delta;
            }
            else if(delta == 0) // Walking on level ground is normal
            {
                return 5;
            }
            else if(delta <= 5) // Walking uphill is tiring
            {
                return 5 + delta;
            }
            else // Climbing up a cliff is very tiring
            {
                return 5 + 2 * delta;
            }
        }

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }
    }
}
