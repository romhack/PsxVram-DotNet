using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsxVram_DotNet.Modes
{
    internal struct TrimConfiguration
    {
        public Rectangle Rectangle;
        public bool IsTransparent;
        public Color[] ClutColors;
        public bool IsInverted;
    }
}
