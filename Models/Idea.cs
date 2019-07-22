using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrightIdeas.Models {
    public class Idea {
        [Key]
        public int IdeaId { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "*** Required ***")]
        [MinLength(5)]
        public string Content { get; set; }
        public User Creator { get; set; }
        public List<Association> UsersWhoLiked { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Idea() {
            UsersWhoLiked = new List<Association>();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}