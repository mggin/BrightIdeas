using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BrightIdeas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BrightIdeas.Controllers {
    public class HomeController : Controller {

        private Resource resource;
        public HomeController(Context context) {
            resource = new Resource(context);
        }

        [Route ("")]
        [HttpGet]
        public IActionResult Index () {
            DataModel Data = new DataModel(){};
            return View (Data);
        }

        [HttpGet("logoff")]
        public IActionResult LogOut() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost ("register")]
        public IActionResult Register(DataModel Data) {
            User user = Data.User;
            if (resource.dbContext.Users.Any (usr => usr.Email == user.Email)) {
                ModelState.AddModelError ("User.Email", "Email already existed!");
            }
            if (ModelState.IsValid) {
                PasswordHasher<User> Hasher = new PasswordHasher<User> ();
                user.Password = Hasher.HashPassword (user, user.Password);
                resource.Add(user);
                HttpContext.Session.SetInt32("userId", user.UserId);
                HttpContext.Session.SetString("login", "true");
                return RedirectToAction ("BrightIdeas");
            }
            return View ("Index");
        }


        [HttpPost("login")]   
        public IActionResult Login (DataModel Data) {
            if (ModelState.IsValid) {
                LoginInfo user = Data.LoginInfo;
                var userInDb = resource.dbContext.Users.FirstOrDefault (usr => usr.Email == user.LEmail);
                if (userInDb == null) {
                    ModelState.AddModelError ("LoginInfo.LEmail", "Invalid Email/Password");
                    return View ("Index");
                }
                var hasher = new PasswordHasher<LoginInfo> ();
                var result = hasher.VerifyHashedPassword (user, userInDb.Password, user.LPassword);
                if (result == 0) {
                    ModelState.AddModelError ("LoginInfo.LEmail", "Invalid Email/Password");
                    return View ("Index");
                }
                System.Console.WriteLine(userInDb.UserId);
                HttpContext.Session.SetInt32("userId", userInDb.UserId);
                HttpContext.Session.SetString("login", "true");
                return RedirectToAction ("BrightIdeas");
            }
            return View ("Index");

        }

        [HttpGet("bright_ideas")]
        public IActionResult BrightIdeas() {
            int currentUserId = (int)  HttpContext.Session.GetInt32("userId");
            DataModel Data = new DataModel() {
                Ideas = resource.GetIdeas(),
                User = resource.GetUserData(currentUserId),

            };
            return View(Data);
        }

        [HttpPost("idea/create")]
        public IActionResult CreateIdea(DataModel Data) {
            int currentUserId = (int)  HttpContext.Session.GetInt32("userId");
            Idea newIdea = Data.Idea;
            newIdea.UserId = currentUserId;
            resource.Add(newIdea);
            return RedirectToAction("BrightIdeas");
        }


        [HttpGet("users/{userId}")]
        public IActionResult Users(int userId) {
            User user = resource.GetUserData(userId);
            return View(user);
        }

        [HttpGet("bright_ideas/{ideaId}")]
        public IActionResult BrightIdea(int ideaId) {
            Idea idea = resource.GetIdeaData(ideaId);
            return View(idea);
        }

        [HttpGet("bright_ideas/{ideaId}/delete")]
        public IActionResult DeleteIdea(int ideaId) {
            Idea idea = resource.GetIdeaData(ideaId);
            resource.Remove(idea);
            return RedirectToAction("BrightIdeas");
        }

        [HttpGet("users/{ideaId}/add_idea")]
        public IActionResult AddIdea(int ideaId) {
            int currentUserId = (int)  HttpContext.Session.GetInt32("userId");
            var ideaExist = resource
            .GetUserData(currentUserId)
            .LikedIdeas
            .Any(_idea => _idea.IdeaId == ideaId);

            if (ideaExist) {
                // ModelState.AddModelError ("Idea", "Only single like is permitted.");
            } else {
                Association newAssociation = new Association() {
                    UserId = currentUserId,
                    IdeaId = ideaId
                };
                resource.Add(newAssociation);
            }
            return RedirectToAction("BrightIdeas");
        }

    }
}