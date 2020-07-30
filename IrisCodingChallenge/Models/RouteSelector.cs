namespace IrisCodingChallenge.Models
{
    public interface RouteSelector
    {
        public WorldGeometry World { get; }

        /// <summary>
        /// Ask this RouteSelector for its preferred route between two castles.
        /// </summary>
        /// <param name="startX">starting X co-ordinate</param>
        /// <param name="startY">starting Y co-ordinate</param>
        /// <param name="endX">X co-ordinate</param>
        /// <param name="endY">Y co-ordinate</param>
        /// <returns></returns>
        string[] GetRoute(int startX, int startY, int endX, int endY);
    }
}
