using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Enchere.Models
{
    public class Objet
    {
        public int Id { get; set; }
        //[Required]
        [Display(Name = "Nom")]
        public string objetNom { get; set; }

        [Display(Name = "Description")]
        [AllowHtml]
        public string objetDescription { get; set; }
        [Display(Name = "Prix de départ")]
        public double objetPrixDepart { get; set; }

        [Display(Name = "Date et heure de l’inscription")]
        public DateTime objetDateInsc { get; set; }

        
         [Display(Name = "Durée de vente")]
        public int objetDureeVente { get; set; }

        [Display(Name = "Date et heure d’achat")]
        public DateTime objetDateAchat { get; set; }

        [Display(Name = "image")]
        public string objetImage { get; set; }

        [Display(Name = "Categorie de l'objet")]
        public int categoryId { get; set; }

        public string UserId { get; set; }

        public virtual Categorie Category { get; set; }
        public virtual ApplicationUser  User { get; set; }
    }
}