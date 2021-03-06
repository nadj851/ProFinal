﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enchere.Models
{
    public class ContactModel
    {

        [Required]
        //[Display(Name = "Nom de Category")]
        public string Nom { get; set; }

        [Required]
        //[Display(Name = "Nom de Category")]
        public string Email{ get; set; }
        [Required]
        //[Display(Name = "Nom de Category")]
        public string Subject { get; set; }
        [Required]
        //[Display(Name = "Nom de Category")]
        [AllowHtml]
        public string Message { get; set; }

    }
}