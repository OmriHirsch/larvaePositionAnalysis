using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace larvaePositionAnalysis
{
    internal class Larva
    {
        public int FrameNumber { get; set; } = 0;
        public string LarvaType { get; set; } = "";
        public PointF Position { get; set; } = Point.Empty;
    }
}
