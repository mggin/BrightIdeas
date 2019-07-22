using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrightIdeas.Models {
    public class Association {
        [Key]
        public int AssociationId { get; set; }
        public int UserId { get; set; }
        public int IdeaId { get; set; }
        public User User { get; set; }
        public Idea Idea { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Association() {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}