using System;
using System.ComponentModel.DataAnnotations;

namespace BrightIdeas.Models{
    public class LoginInfo {

        [Required(ErrorMessage = "*** Required ***")]
        public string LEmail { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "*** Required ***")]
        public string LPassword { get; set; }
    }
}