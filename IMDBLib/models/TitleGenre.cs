using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBLib.models
{
    public class TitleGenre
    {
        public string tconst { get; set; }
        public string? genreName { get; set; }

        public TitleGenre(string tconst, string? genreName)
        {
            this.tconst = tconst;
            this.genreName = genreName;
        }
    }
}
