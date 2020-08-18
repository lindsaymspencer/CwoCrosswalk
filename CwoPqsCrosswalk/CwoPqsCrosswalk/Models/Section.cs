using System.Collections.Generic;

namespace CwoPqsCrosswalk.Models
{
    public class Section
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public Dictionary<string, bool> Signatures { get; set; }
    }
}
