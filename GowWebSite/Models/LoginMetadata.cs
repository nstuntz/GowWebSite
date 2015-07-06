using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GowWebSite.Models
{
    [MetadataType(typeof(LoginMetadata))]
    public partial class Login { }

    public class LoginMetadata
    {
        [EmailAddress]
        [Required]
        public string UserName { get; set; }

        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        [Required(ErrorMessage="Password if required.")]
        public string Password { get; set; }
    }
}