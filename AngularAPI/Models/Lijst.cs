using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAPI.Models
{
    public class Lijst
    {
        public long LijstID { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public ICollection<Item> Items { get; set; }
        public long GebruikerID { get; set; }

    }
}
