
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Scrypt;
using YoloBlogger.Models;
using YoloBlogger.ViewModels;
using YoloBlogger.Extensions;

namespace YoloBlogger.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult SaveData(UserDetails userDetails)
        {
            ScryptEncoder encode = new ScryptEncoder();
            UserDetailsContext context = new UserDetailsContext();
           
            if (!ModelState.IsValid)
            {
                return View("Index", userDetails);
            }

            UserDetails userExistence = context.UserDetails.SingleOrDefault(x=>x.Email==userDetails.Email);
            
          
            
            if (userExistence == null)
            {
                ViewBag.UserAlreadyExists = "User Does Not Exists";
                return View("Index");
            }
            else if (!encode.Compare(userDetails.Password, userExistence.Password))
            {
                ViewBag.UserAlreadyExists = "Incorrect Password";
                return View("Index");
            }
            else if (encode.Compare(userDetails.Password, userExistence.Password))
            {
                Session["UserId"] = userExistence.Id;
                Session["Email"] = userExistence.Email;
                Session["UserName"] = userExistence.UserName;
                ViewBag.msg = userExistence.UserName;
                return RedirectToAction("Main", "Home");
            }
            return View("Index");
        }

        public ActionResult Main()
        {

            UserDetailsContext context = new UserDetailsContext();
            if (Session["UserId"] != null)
            {


                var userId = Convert.ToInt32(Session["UserId"]);
                var existence = context.UserProfileDetails.SingleOrDefault(x => x.UserId == userId);
                if (existence == null)
                {
                    ViewBag.NoUserProfile = "Please Create a UserName to Continue";
                    return View("Edit");
                }
                else
                {
                    ViewBag.Msg = Session["UserName"];
                    var post = context.Posts.Include("Comments").OrderByDescending(x=>x.PostDateTime).ToList();
                    var like = context.LikesDetails.Where(x => x.UserId == userId).ToList();
                    ProfilepageViewModel profilepageViewModel = new ProfilepageViewModel
                    {
                        Posts = post,
                        LikesDetails=like
                    };
                    return View(profilepageViewModel);

                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            ScryptEncoder encode = new ScryptEncoder();
            UserDetailsContext context = new UserDetailsContext();
            if (!ModelState.IsValid)
            {
                return View("SignUp", registerViewModel);
            }
            
            var userExistence = context.UserDetails.SingleOrDefault(x => x.Email == registerViewModel.UserDetails.Email);
            if (userExistence != null)
            {
                ViewBag.UserAlreadyExists = "User Already Exists";
                return View("SignUp", registerViewModel);
            }
            if (registerViewModel.ConfirmPassword != registerViewModel.UserDetails.Password)
            {
                ViewBag.UserAlreadyExists = "Password And Confirm Password Mismatch";
                return View("SignUp", registerViewModel);
            }
            registerViewModel.UserDetails.Password = encode.Encode(registerViewModel.UserDetails.Password);
            registerViewModel.UserDetails.UserName = " ";
            context.UserDetails.Add(registerViewModel.UserDetails);
            context.SaveChanges();
            // @ViewBag.success = "Account Created Succesfully Login to continue";
            this.AddNotification("Account Created Succesfully Login to continue", NotificationType.SUCCESS);
            return View("SignUp");

        }

        public ActionResult ProfilePage()
        {
            UserDetailsContext context = new UserDetailsContext();
            if (Session["UserId"] != null)
            {

                var UserId = Convert.ToInt32(Session["UserId"]);
                ViewBag.msg = Session["UserName"];

                var details = context.UserProfileDetails.Include("UserDetailss").SingleOrDefault(x => x.UserId == UserId);
                var posts = context.Posts.Where(x => x.UserId == UserId).OrderByDescending(x=>x.PostDateTime).ToList();
                ProfilepageViewModel profilepageViewModel = new ProfilepageViewModel
                {
                    UserProfileDetails = details,
                    Posts = posts
                };
                return View(profilepageViewModel);
            }
            else
                return RedirectToAction("Index", "Home");
        }
        public ActionResult ProfileP(int id)
        {
            UserDetailsContext context = new UserDetailsContext();           
            ViewBag.msg = Session["UserName"];
            var details = context.UserProfileDetails.Include("UserDetailss").SingleOrDefault(x => x.UserId == id);
            var posts = context.Posts.Where(x => x.UserId == id).OrderByDescending(x=>x.PostDateTime).ToList();
            ProfilepageViewModel profilepageViewModel = new ProfilepageViewModel
            {
                UserProfileDetails = details,
                Posts = posts
            };
            return View("ProfilePage", profilepageViewModel);
        }
        public ActionResult Edit()
        {
            UserDetailsContext context = new UserDetailsContext();

            var userId = Convert.ToInt32(Session["UserId"]);
            var userExistence = context.UserProfileDetails.SingleOrDefault(x => x.UserId == userId);
            if (userExistence == null)
                return View();
            UserProfileDetails UserProfileDetails = new UserProfileDetails
            {
                UserName = userExistence.UserName,
                Bio = userExistence.Bio
            };
            //  return Content(UserProfileDetails.UserName);
            return View(UserProfileDetails);
        }
        public ActionResult SaveEdit(UserProfileDetails userProfileDetails)
        {
            UserDetailsContext context = new UserDetailsContext();
            UserDetailsContext contextt = new UserDetailsContext();
            UserDetailsContext contexttt = new UserDetailsContext();
            UserDetails userDetails = new UserDetails();
            if (!ModelState.IsValid)
            {
                return View("Edit", userProfileDetails);
            }

            var userId = Convert.ToInt32(Session["UserId"]);
            var userExistence = context.UserProfileDetails.SingleOrDefault(x => x.UserId == userId);
            var userExistenceInMain = contextt.UserDetails.SingleOrDefault(x => x.Id == userId);
            if(contexttt.UserProfileDetails.Any(x=>x.UserName==userProfileDetails.UserName && x.UserId!=userId))
            {
                ViewBag.error = "UserName already Exists . Try With other name";
                return View("Edit");
            }
            if (userExistence == null)
            {
                userProfileDetails.UserId = userId;
                context.UserProfileDetails.Add(userProfileDetails);
                context.SaveChanges();
                
                userExistenceInMain.UserName = userProfileDetails.UserName;
                contextt.SaveChanges();
                return RedirectToAction("ProfilePage", "Home");
            }

            userExistence.UserName = userProfileDetails.UserName;
            userExistence.Bio = userProfileDetails.Bio;
            context.SaveChanges();
            userExistenceInMain.UserName = userProfileDetails.UserName;
            contextt.SaveChanges();
            
            return RedirectToAction("ProfilePage", "Home");
        }
        public ActionResult About()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "Home");
            else if (Session["UserName"] == null)
            {
                ViewBag.NoUserProfile = "Please Create a UserName to Continue";
                return View("Edit");
            }
            ViewBag.msg = Session["UserName"];
            return View();
        }
        public ActionResult Contact()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "Home");
            else if (Session["UserName"] == null)
            {
                ViewBag.NoUserProfile = "Please Create a UserName to Continue";
                return View("Edit");
            }
            ViewBag.msg = Session["UserName"];
            return View();
        }
        public ActionResult AddPost()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "Home");
            ViewBag.msg = Session["UserName"];
            return View();
        }
          public ActionResult SavePost(Posts posts)
          {
              UserDetailsContext context = new UserDetailsContext();
              posts.UserId = Convert.ToInt32(Session["UserId"]);
              if (!ModelState.IsValid)
              {
                  return View("AddPost", posts);
              }
              posts.UserName = Session["UserName"].ToString();
              posts.PostDateTime = DateTime.Now;
              context.Posts.Add(posts);
              context.SaveChanges();
              this.AddNotification("Post Successfully Uploaded", NotificationType.SUCCESS);
              return RedirectToAction("Main", "Home");
          }   
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Comments(int id)
        {
            UserDetailsContext context = new UserDetailsContext();
            var comment = context.Comments.Include("UserDetailss").Where(x=>x.PostId==id).ToList();
            return PartialView("_Comments", comment);
        }
        public ActionResult Like(int id)
        {
            UserDetailsContext context = new UserDetailsContext();
            var userId = Convert.ToInt32(Session["UserId"]);
          
                LikesDetails likesDetails = new LikesDetails
                {
                    PostId = id,
                    UserId = userId
                };
                context.LikesDetails.Add(likesDetails);
                context.SaveChanges();
                var count = context.LikesDetails.Where(x=>x.PostId==id).Count();
                var post = context.Posts.Find(id);
                post.Like = count;
                context.SaveChanges();
                return RedirectToAction("Main", "Home");
            
            
        }
        public ActionResult Dislike(int id)
        {
            UserDetailsContext context = new UserDetailsContext();
            var userId = Convert.ToInt32(Session["UserId"]);
            var post = context.LikesDetails.SingleOrDefault(x => x.PostId == id && x.UserId == userId);
            context.LikesDetails.Remove(post);
            context.SaveChanges();
            var like=context.Posts.Find(id);
            like.Like -= 1;
            context.SaveChanges();
            return RedirectToAction("Main", "Home");
        }
        public ActionResult AddComment(ProfilepageViewModel profilepageViewModel,string buttonvalue)
        {
            var userId = Session["UserId"];
            var postId = buttonvalue;
            UserDetailsContext context = new UserDetailsContext();
            Comments comments = new Comments
            {
                PostId = Convert.ToInt32(postId),
                UserId = Convert.ToInt32(userId),
                Comment=profilepageViewModel.UserComment

            };
            context.Comments.Add(comments);
            context.SaveChanges();
            return RedirectToAction("Main","Home");

        }
        public ActionResult DeletePost(int id)
        {
            UserDetailsContext context = new UserDetailsContext();
            var post = context.Posts.SingleOrDefault(x => x.PostId == id);
            var postC = context.Comments.Where(x => x.PostId == id).ToList(); ;
            context.Comments.RemoveRange(postC);
            context.SaveChanges();
            context.Posts.Remove(post);
            context.SaveChanges();
            return RedirectToAction("ProfilePage", "Home");
        }
    }
}