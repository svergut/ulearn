using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class Sizes
    {
        public static Dictionary<string, Size> Collection = new Dictionary<string, Size>()
        {
            ["game"] = new Size(1600, 650),
            ["xwing"] = new Size(150, 50),
            ["blast"] = new Size(60, 10),
            ["obstacle"] = new Size(50, 50)
        };
    }
}
