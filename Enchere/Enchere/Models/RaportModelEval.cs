using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enchere.Models
{
    public class RaportModelEval
    {
        [Display(Name = "Membre")]
        public string Membre  { get; set; }
        [Display(Name = "Nombre Evaluations")]
        public int NbrEval { get; set; }
        [Display(Name = "Cote")]
        public double CotePo { get; set; }


    }
}