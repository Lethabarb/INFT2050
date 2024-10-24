using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDLibrary.Entities
{
    public class Track
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Index { get; set; }
        public int CdId { get; set; }
        [Ignore]
        public CD CD { get; set; }
        [Ignore]
        public string CdColour
        {
            get
            {
                return CD.Color;
            }
        }
        [Ignore]
        public string CdName
        {
            get
            {
                return CD.Name;
            }
        }
        [Ignore]
        public int CdIndex
        {
            get
            {
                return CD.Index;
            }
        }
        public string Note1 { get; set; }
        public string Note2 { get; set; }
        public string Note3 { get; set; }

        public bool RelatedText(string text)
        {
            if (Title.Contains(text)) return true;
            if (CD.Name.Contains(text)) return true;
            if (Note1.Contains(text)) return true;
            if (Note2.Contains(text)) return true;
            if (Note3.Contains(text)) return true;
            return false;
        }
    }
}
