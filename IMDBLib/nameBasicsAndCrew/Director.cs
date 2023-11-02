using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBLib.nameBasicsAndCrew
{
    public class Director
    {
        public string nconst { get; set; }
        public string tconst { get; set; }

        public Director(string nconst, string tconst)
        {
            this.tconst = tconst;
            this.nconst = nconst;
        }
    }
}
