using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDLibrary.Entities
{
    public class CD
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Index {  get; set; }
        public string Color { get; set; }

        [Ignore]
        public List<Track> Tracks { get; set; }
    }
}
