using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoteMarketPlace.Models
{
    public class UserLogin
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Id Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }



    //[Required(AllowEmptyStrings = false, ErrorMessage = "First Name Required")]
    //public string FirstName { get; set; }
    //[Required(AllowEmptyStrings = false, ErrorMessage = "Last Name Required")]
    //public string LastName { get; set; }
    //[Required(AllowEmptyStrings = false, ErrorMessage = "Email Id Required")]
    //[DataType(DataType.EmailAddress)]
    //public string Email { get; set; }
    //[Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
    //[DataType(DataType.Password)]

    //public string Password { get; set; }
    //[Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
    //[DataType(DataType.Password)]
    //[Compare("Password", ErrorMessage = "Confirm password and password do not match")]
    //public string ConfirmPassword { get; set; }

    //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]

}