using System;
using BrightIdeas.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace BrightIdeas {
   public class Resource {
      public Context dbContext { get; set; }
      public Resource(Context _dbContext) {
         dbContext = _dbContext;
      }

      public User GetUserData(int userId) {
         User user = dbContext.Users
            .Include(_user => _user.CreatedIdeas)
            .Include(_user => _user.LikedIdeas)
            .ThenInclude(associate => associate.Idea)
            .FirstOrDefault(_user => _user.UserId == userId);
         return user;        
      }

      public Idea GetIdeaData(int ideaId) {
         Idea idea = dbContext.Ideas
            .Include(_idea => _idea.Creator)
            .Include(_idea => _idea.UsersWhoLiked)
            .ThenInclude(associate => associate.User)
            .FirstOrDefault(_idea => _idea.IdeaId == ideaId);
         return idea;        
      }

      public List<Idea> GetIdeas() {
         var ideas = dbContext.Ideas
         .Include(idea => idea.Creator)
         .Include(idea => idea.UsersWhoLiked)
         .ThenInclude(associate => associate.User)
         .OrderByDescending(idea => idea.UsersWhoLiked.Count)
         .ToList();
         return ideas;
      }



      public void Add(Object obj) {
         dbContext.Add(obj);
         dbContext.SaveChanges();
      }

      public void Remove(Object obj) {
         dbContext.Remove(obj);
         dbContext.SaveChanges();
      }


   }
}