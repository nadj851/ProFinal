using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enchere.Models
{
    public enum enumStatutObjet
    {
        [Display(Name = "En vente.")]
        EV,
        [Display(Name = "Vendu.")]
        VD,
        [Display(Name = "Expiré.")]
        EX,
    }
}