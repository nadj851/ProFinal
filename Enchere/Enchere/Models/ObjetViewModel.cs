using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enchere.Models
{
    public class ObjetViewModel
    {

        public string ObjetNom { get; set; }
        public IEnumerable<Encheree> Items{ get; set; }
    }
}