using IrisCodingChallenge.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IrisCodingChallengeTests
{
    [TestClass]
    public class DijkstraRouteSelectorTests
    {
        [TestMethod]
        public void DijkstraRouteSelector_CanFindPaths()
        {
            WorldGeometry worldMap = new GraphWorldMap(100, 100, 0, 50);
            RouteSelector routeSelector = new DijkstraRouteSelector((GraphWorldMap)worldMap);

            string[] routeSteps = routeSelector.GetRoute(40, 2, 40, 85);

            foreach(string step in routeSteps)
            {
                Console.WriteLine(step);
            }
        }
    }
}
