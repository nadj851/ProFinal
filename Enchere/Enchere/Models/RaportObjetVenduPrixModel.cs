using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enchere.Models
{
    public class RaportObjetVenduPrixModel
    {

        public string MembreVendeur { get; set; }
        public string  ObjetName { get; set; }
        public string ObjetCategorie { get; set; }
        public double PrixInitial{ get; set; }
        public double PrixFinal { get; set; }
    }
}