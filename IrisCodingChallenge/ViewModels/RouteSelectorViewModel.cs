using IrisCodingChallenge.Models;
using System.ComponentModel;

namespace IrisCodingChallenge.ViewModels
{
    public class RouteSelectorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RouteSelector RouteSelector { get; set; }
        public WorldGeometry World { get; set; }
        public Vertex StartVertex { get; private set; }
        public Vertex EndVertex { get; private set; }
        public Route Route { get; set; }

        public RouteSelectorViewModel()
        {
        }

        public void Generate()
        {
            GraphWorldMap worldMap = new GraphWorldMap(30, 30, 4, 15);
            DijkstraRouteSelector routeSelector = new DijkstraRouteSelector(worldMap);

            StartVertex = worldMap.GetVertex(27, 2);
            EndVertex = worldMap.GetVertex(5, 26);
            Route = routeSelector.GetRoute(StartVertex, EndVertex);

            RouteSelector = routeSelector;
            World = worldMap;

            NotifyPropertyChanged(nameof(StartVertex));
            NotifyPropertyChanged(nameof(EndVertex));
            NotifyPropertyChanged(nameof(Route));
            NotifyPropertyChanged(nameof(RouteSelector));
            NotifyPropertyChanged(nameof(World));
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
