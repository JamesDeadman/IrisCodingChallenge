namespace IrisCodingChallenge.Models
{
    public class DijkstraRouteTableRow
    {
        public Vertex EndVertex { get; private set; }
        public int Distance { get; set; } = int.MaxValue;
        public Vertex PreviousVertex { get; set; }

        public DijkstraRouteTableRow(Vertex endVertex)
        {
            EndVertex = endVertex;
        }

        public override string ToString()
        {
            return $"E:{EndVertex} D:{Distance} P:{PreviousVertex}";
        }
    }
}
