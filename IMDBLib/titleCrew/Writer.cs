using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBLib.titleCrew
{
    public class Writer
    {
        public string nconst { get; set; }
        public string tconst { get; set; }

        public Writer(string nconst, string tconst)
        {
            this.nconst = nconst;
            this.tconst = tconst;
        }
    }
}
