using System;
using System.Linq;
using System.Collections.Generic;

namespace IrisCodingChallenge.Models
{
    public class DijkstraRouteSelector : RouteSelector
    {
        private GraphWorldMap worldMap;

        public WorldGeometry World => worldMap;

        public DijkstraRouteSelector(GraphWorldMap worldMap)
        {
            this.worldMap = worldMap;
        }

        public Route GetRoute(Vertex startVertex, Vertex endVertex)
        {
            Route route = new Route();

            Dictionary<Vertex, DijkstraRouteTableRow> table = new Dictionary<Vertex, DijkstraRouteTableRow>();
            HashSet<Vertex> visitedVertices = new HashSet<Vertex>();
            HashSet<Vertex> unvisitedVerticies = new HashSet<Vertex>();

            // Setup starting conditions for Dijkstra
            foreach (Vertex vertex in worldMap.AllVerticies)
            {
                unvisitedVerticies.Add(vertex);
                table.Add(vertex, new DijkstraRouteTableRow(vertex));
            }

            // Initialize the first vertex
            Vertex visitingVertex = startVertex;
            table[visitingVertex].Distance = 0;

            // Loop until all have been visited
            while (unvisitedVerticies.Count > 0)
            {
                // Get the vertex with the shortest distance
                visitingVertex = unvisitedVerticies.OrderBy(v => table[v].Distance).First();

                // Apply distance to nodes at each outbound edge
                foreach (Edge visitedEdge in visitingVertex.OutboundEdges)
                {
                    Vertex nextVertex = visitedEdge.EndVertex;

                    // Check if this is a new shortest distance to this vertex
                    if (table[nextVertex].Distance > table[visitingVertex].Distance + visitedEdge.Weight)
                    {
                        table[nextVertex].PreviousVertex = visitingVertex;
                        table[nextVertex].Distance = table[visitingVertex].Distance + visitedEdge.Weight;
                    }
                }

                // Don't visit this vertex again
                unvisitedVerticies.Remove(visitingVertex);
                visitedVertices.Add(visitingVertex);
            }

            // Unwind the shortest path from the end vertex
            Vertex pathVertex = endVertex;
            do
            {
                route.Verticies.Add(pathVertex);
                pathVertex = table[pathVertex].PreviousVertex;
            } while (pathVertex != startVertex);
            route.Verticies.Add(pathVertex);
            route.Verticies.Reverse();

            return route;
        }

        public string[] GetRoute(int startX, int startY, int endX, int endY)
        {
            Vertex startVertex = worldMap.GetVertex(startX, startY);
            Vertex endVertex = worldMap.GetVertex(endX, endY);

            if(startVertex == null)
            {
                throw new ArgumentException("Start vertex is outside of the world bounds");
            }

            if (endVertex == null)
            {
                throw new ArgumentException("End vertex is outside of the world bounds");
            }

            return GetRoute(startVertex, endVertex).ToDirections();
        }
    }
}
