using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace larvaePositionAnalysis
{
    internal class Frame
    {
        public int FrameNumber { get; set; }
        public List<Larva> Larvae { get; set; } = new List<Larva>();

    }
}
