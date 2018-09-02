using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Web.Models
{
    public class RecoveryPassViewModel
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
    }
}