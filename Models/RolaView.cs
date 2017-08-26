using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MontazneKuce.Models
{
    public class RolaView
    {
        public string Id { get; set; }
        public string Naziv { get; set; }
        public IEnumerable<ApplicationUser> BrojKorisnika { get; set; }
    }
}