using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoteMarketPlace.Models
{
    public class ResetPassword
    {

        [Required(ErrorMessage = "new password required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]

        public string OldPassword { get; set; }
        [Required(ErrorMessage ="new password required",AllowEmptyStrings =false)]
        [DataType(DataType.Password)]

        public string NewPassword { get; set; }
        [Required(ErrorMessage = "new password required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="new password and confirm password does not match")]
        public string ConfirmPassword { get; set; }
     
    }
}