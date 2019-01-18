using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HopesPartners_alpha.Models
{
    public class ContentModels
    {
        [Display(Name ="Title of Document")]
        public String Title { get; set; }

        [Display(Name ="Link")]
        public String WebAddress { get; set; }

        [Display(Name ="Date Posted")]
        public DateTime DateCreated { get; set; }

    }
}