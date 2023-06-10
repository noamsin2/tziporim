using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tziporim
{
    public class Cage
    {
        public string SN { get; set; }
        public string length { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public string material { get; set; }
        public Cage(string SN, string length, string width, string height, string material)
        {
            this.SN = SN;
            this.length = length;
            this.width = width;
            this.height = height;
            this.material = material;
        }
    }
}
