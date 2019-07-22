using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BrightIdeas.Models{
    public class DataModel {

        public User User { get; set; }
        public Idea Idea { get; set; }
        public LoginInfo LoginInfo { get; set; }
        public Association Association { get; set; }
        public List<User> Users { get; set; }
        public List<Idea> Ideas { get; set; }
        public List<Association> Associations { get; set; }

    }
}