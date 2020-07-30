using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using IrisCodingChallenge.Models;

namespace IrisCodingChallenge.Views
{
    public class RouteSelectorVisualizer: FrameworkElement
    {
        public static readonly DependencyProperty WorldGeometryProperty = 
            DependencyProperty.Register(nameof(WorldGeometry), typeof(WorldGeometry), typeof(RouteSelectorVisualizer), new PropertyMetadata(null));
        public WorldGeometry WorldGeometry
        {
            get => (WorldGeometry)GetValue(WorldGeometryProperty);
            set
            {
                SetValue(WorldGeometryProperty, value);
                Redraw();
            }
        }

        public static readonly DependencyProperty RouteProperty =
            DependencyProperty.Register(nameof(Route), typeof(Route), typeof(RouteSelectorVisualizer), new PropertyMetadata(null));
        public Route Route
        {
            get => (Route)GetValue(RouteProperty);
            set
            {
                SetValue(RouteProperty, value);
                Redraw();
            }
        }

        public static readonly DependencyProperty StartVertexProperty =
            DependencyProperty.Register(nameof(StartVertex), typeof(Vertex), typeof(RouteSelectorVisualizer), new PropertyMetadata(null));
        public Vertex StartVertex
        {
            get => (Vertex)GetValue(StartVertexProperty);
            set
            {
                SetValue(StartVertexProperty, value);
                Redraw();
            }
        }

        public static readonly DependencyProperty EndVertexProperty =
            DependencyProperty.Register(nameof(EndVertex), typeof(Vertex), typeof(RouteSelectorVisualizer), new PropertyMetadata(null));
        public Vertex EndVertex
        {
            get => (Vertex)GetValue(EndVertexProperty);
            set
            {
                SetValue(EndVertexProperty, value);
                Redraw();
            }
        }

        public RouteSelectorVisualizer()
        {
        }

        public void Redraw()
        {
            UpdateLayout();
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext context)
        {
            if (WorldGeometry != null)
            {
                DrawWorld(context);
                if (Route != null)
                {
                    DrawRoute(context);
                }
            }
        }

        private void DrawRoute(DrawingContext context)
        {
            Vertex previousVertex = Route.Verticies.First();
            Pen pen = new Pen(Brushes.Yellow, 4);

            double vertexWidth = ActualWidth / (WorldGeometry.XMax + 1);
            double vertexHeight = ActualHeight / (WorldGeometry.YMax + 1);

            Point startPoint = new Point(vertexWidth * (previousVertex.X + .5), vertexHeight * (previousVertex.Y + .5));

            foreach (Vertex vertex in Route.Verticies.Skip(1))
            {
                Point point1 = new Point(vertexWidth * (vertex.X + .5), vertexHeight * (vertex.Y + .5));
                Point point2 = new Point(vertexWidth * (previousVertex.X + .5), vertexHeight * (previousVertex.Y + .5));
                context.DrawLine(pen, point1, point2);
                previousVertex = vertex;
            }

            Point endPoint = new Point(vertexWidth * (previousVertex.X + .5), vertexHeight * (previousVertex.Y + .5));
            
            context.DrawEllipse(Brushes.Red, null, startPoint, vertexWidth / 4, vertexHeight / 4);
            context.DrawEllipse(Brushes.Blue, null, endPoint, vertexWidth / 4, vertexHeight / 4);
        }

        private void DrawWorld(DrawingContext context)
        {
            Rect drawingArea = new Rect(0, 0, ActualWidth, ActualHeight);
            context.DrawRectangle(Brushes.LightGray, new Pen(Brushes.Black, 1), drawingArea);

            int maxX = WorldGeometry.XMax;
            int maxY = WorldGeometry.YMax;

            double vertexWidth = ActualWidth / (maxX + 1);
            double vertexHeight = ActualHeight / (maxY + 1);
            int maxElevation = (WorldGeometry as GraphWorldMap)?.MaxElevation ?? 100;

            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    int elevation = WorldGeometry.GetElevation(x, y);
                    Rect gridSpace = new Rect(x * vertexWidth, y * vertexHeight, vertexWidth, vertexHeight);
                    byte normalizedHeight = (byte)Math.Min(byte.MaxValue * elevation / maxElevation, byte.MaxValue);
                    Brush brush = new SolidColorBrush(Color.FromArgb(255, 0, normalizedHeight, 0));
                    context.DrawRectangle(brush, new Pen(brush, 1), gridSpace);
                }
            }
        }
    }
}