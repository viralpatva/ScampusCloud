using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScampusCloud.Models
{
    public class LoginModel : ResponseMessage
    {
        public LoginModel()
        {

        }
        [Required(ErrorMessage = "Username is required")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        //[RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,20}$", ErrorMessage = "Your password must be at least 6 to 20 characters long")]
        //[RegularExpression(@"(?=.*\d)(?=.*[A-Za-z]).{8,}",
        //    ErrorMessage = "Your password must be at least 8 characters long and contain at least 1 letter and 1 number")]
        public string Password { get; set; }

    }
}