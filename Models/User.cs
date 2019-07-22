using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrightIdeas.Models {
    public class User {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "*** Required ***")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Invalid input, only letter and spaces")]

        public string Name { get; set; }
        [Required(ErrorMessage = "*** Required ***")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Invalid input, only letter and number")]
        public string Alias { get; set; }
        [Required(ErrorMessage = "*** Required ***")]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "*** Required ***")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }
        [Required(ErrorMessage = "*** Required ***")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password didn't match")]
        [NotMapped]
        public string Confirm { get; set; }
        public List<Association> LikedIdeas { get; set; }
        public List<Idea> CreatedIdeas { get; set; }  
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User() {
            LikedIdeas = new List<Association>();
            CreatedIdeas = new List<Idea>();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}