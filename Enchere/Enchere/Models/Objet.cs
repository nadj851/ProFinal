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

        [Display(Name = "Déscription")]
        [AllowHtml]
        public string objetDescription { get; set; }
        [Display(Name = "Prix")]
        public double objetPrixDepart { get; set; }

        [Display(Name = "Date inscription")]
        public DateTime objetDateInsc { get; set; }     

                
        [Display(Name = "Durée Vente")]
        public int objetDureeVente { get; set; }

        [Display(Name = "Date achat")]
        public DateTime objetDateAchat { get; set; }

        [Display(Name = "Statut")]
        public string Statut{ get; set; }


        [Display(Name = "image")]
        public string objetImage { get; set; }

        [Display(Name = "Catégorie")]
        public int categoryId { get; set; }

        public string UserId { get; set; }

        public virtual Categorie Category { get; set; }
        public virtual ApplicationUser  User { get; set; }

        
    }
    
}