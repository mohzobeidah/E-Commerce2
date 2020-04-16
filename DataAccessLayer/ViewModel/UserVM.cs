using DataAccessLayer.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Text;
using DataAccessLayer.Enum;

namespace DataAccessLayer.ViewModel
{
   public  class UserVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]

        [DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        public Country.eCountry Country { get; set; }

        public bool AgreeTerm { get; set; }

        public List<SelectListItem> Counties
        {
            get
            {
                Array values = System.Enum.GetValues(typeof(Country.eCountry));
                List<SelectListItem> items = new List<SelectListItem>(values.Length);

                foreach (var i in values)
                {
                    items.Add(new SelectListItem
                    {
                        Text =  System.Enum.GetName(typeof(Country.eCountry), i) ,
                        Value = i.ToString()
                    });
                }

                return items;
            }


        }



    }
}
