﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Enchere.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nom de Category")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string  CategoryDescription { get; set; }

        public virtual ICollection<Objet> objets { get; set; }
    }
}