namespace IrisCodingChallenge.Models
{
    public class Edge
    {
        public Vertex StartVertex { get; private set; }
        public Vertex EndVertex { get; private set; }
        public Vertex[] Verticies => new Vertex[] { StartVertex, EndVertex };
        public int Weight { get; private set; }

        public Edge(Vertex startVertex, Vertex endVertex, int weight)
        {
            StartVertex = startVertex;
            EndVertex = endVertex;
            Weight = weight;
        }
    }
}
