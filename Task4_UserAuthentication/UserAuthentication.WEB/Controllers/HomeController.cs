namespace UserAuthentication.WEB.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Data;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    // [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetTable")]
        public ActionResult GetTable(int? userState, int? provider)
        {
            var socialNetWork = GetSocialNetWork(provider);
            var status = GetStatus(userState);

            var users = GetUsers(status, socialNetWork);

            return PartialView("GetTable", users);
        }

        private List<UserView> GetUsers(int? status, string socialNetWork)
        {
            List<UserView> users;
            if (status != null)
            {
                users = _dbContext.Users
                    .Join(_dbContext.UserLogins, user => user.Id, login => login.UserId,
                        (user, login) => new UserView
                        {
                            LoginProvider = login.LoginProvider, Email = user.Email, Active = user.LockoutEnabled,
                            FirstLogin = user.FirstLogin, LastLogin = user.LastLogin
                        })
                    .Where(login => socialNetWork.Contains(login.LoginProvider))
                    .Where(state => Convert.ToBoolean(status) == state.Active).ToList();
            }
            else
            {
                users = _dbContext.Users
                    .Join(_dbContext.UserLogins, user => user.Id, login => login.UserId,
                        (user, login) => new UserView
                        {
                            LoginProvider = login.LoginProvider, Email = user.Email, Active = user.LockoutEnabled,
                            FirstLogin = user.FirstLogin, LastLogin = user.LastLogin
                        })
                    .Where(login => socialNetWork.Contains(login.LoginProvider)).ToList();
            }

            return users;
        }

        private static int? GetStatus(int? userState)
        {
            switch (userState)
            {
                case 1:
                    return 0;
                case 2:
                    return 1;
                default:
                    return null;
            }
        }

        private static string GetSocialNetWork(int? provider)
        {
            switch (provider)
            {
                case 1:
                    return "Facebook";
                case 2:
                    return "Google";
                case 3:
                    return "Twitter";
                default:
                    return "FacebookGoogleTwitter";
            }
        }
    }

    public class UserView
    {
        public DateTime? FirstLogin { get; set; }

        public DateTime? LastLogin { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string LoginProvider { get; set; }
    }
}