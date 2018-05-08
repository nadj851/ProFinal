using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Enchere.Models
{
    public class Encheree
    {

        public int Id { get; set; }

        [Display(Name = "Montant")]
        public double enchereNiveau { get; set; }
        public string Message { get; set; }
        public DateTime enchereDate { get; set; }
        public int ObjetId { get; set; }
        public string UserId { get; set; }
        public virtual Objet objet { get; set; }
        public virtual ApplicationUser user { get; set; }
    }
}