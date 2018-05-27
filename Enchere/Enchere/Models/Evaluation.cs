using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Enchere.Models
{
    public class Evaluation
    {
        public int Id { get; set; }

        public DateTime DateEvaluation { get; set; }
        [Required]
        public double Cote { get; set; }
        public double TotalCote { get; set; }
        [Required]
        [AllowHtml]
        public string Commentaire { get; set; }
        public string Vendeur { get; set; }
        [Display(Name = "Acheteur")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}