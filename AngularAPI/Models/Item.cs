    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAPI.Models
{
    public class Item
    {
        public long ItemID { get; set; }
        public long LijstID { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public ICollection<Stem> stemmen { get; set; }
    }
}
