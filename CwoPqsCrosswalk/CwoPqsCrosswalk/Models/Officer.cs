using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CwoPqsCrosswalk.Models
{
    public class Officer
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = "Lindsay";

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = "Spencer";

        public string Rank { get; set; } = "LTJG";

        public CwoPqs TwoBravo { get; set; } = CwoPqs.NewTwoBravo();

        public CwoPqs TwoAlpha { get; set; } = CwoPqs.NewTwoAlpha();

    }
}
