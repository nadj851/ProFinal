using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Enchere.Models
{
    public class Evaluation
    {
        public int Id { get; set; }
        public DateTime DateEvaluation { get; set; }
        public double Cote { get; set; }
        public string Commentaire { get; set; }
               
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}