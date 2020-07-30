using System;
using System.Collections.Generic;
using System.Text;

namespace IrisCodingChallenge.Models
{
    public interface WorldGeometry
    {
        public int XMax { get; }
        public int YMax { get; }
        public int GetElevation(int x, int y);
    }
}
