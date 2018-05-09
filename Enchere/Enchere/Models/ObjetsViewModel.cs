using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enchere.Models
{
    public class ObjetsViewModel
    {
        public IList<Categorie> Categorie { get; set; }
        public IList<Encheree> Enchere { get; set; }
    }
}