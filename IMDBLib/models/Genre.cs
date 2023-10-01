using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBLib.models
{
    public class Genre
    {
        public string genreName { get; set; }

        public Genre(string genreName)
        {
            this.genreName = genreName;
        }
    }
}
