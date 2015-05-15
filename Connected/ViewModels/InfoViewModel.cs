using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Connected.ViewModels
{
    public class InfoViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Full name")]
        public String FullName { get; set; }
        [Display(Name = "About me")]
        public string Description { get; set; }
        [Display(Name = "Image")]
        public string Image { get; set; }
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Display(Name = "Age")]
        public int Age { get; set; }
    }
}