using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HopesPartners_alpha.Models
{
    public class UserModels : ApplicationUser
    {
        [Required]
        [EmailAddress]
        public override string Email { get; set; }

        [Display(Name = "Partner Since")]
        public string DateTime { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        private List<string> _roles = new List<string>() { "Partner", "Partner" };
        public IEnumerable<SelectListItem> RoleTypes
        {
            get { return new SelectList(_roles); }
        }
    }
}