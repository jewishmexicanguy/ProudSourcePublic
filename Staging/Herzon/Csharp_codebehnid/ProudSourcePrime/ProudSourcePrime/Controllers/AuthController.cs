using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using ProudSourcePrime.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ProudSourcePrime.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        ApplicationUserManager appUserManager;
        /// <summary>
        /// Class constructor
        /// </summary>
        public AuthController()
        {
            appUserManager = new ApplicationUserManager(new UserStore<IdentityUser>());
        }

        // GET: Auth/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnURL)
        {
            Models.LogInModel model = new Models.LogInModel()
            {
                ReturnURL = returnURL
            };
            return View(model);
        }

        // POST: Auth/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(Models.LogInModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            IdentityUser user = await appUserManager.FindByNameAsync(model.Email);
            if (user.Id != null)
            {
                var result = appUserManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, model.Password);
                if (result.Equals(PasswordVerificationResult.Success))
                {
                    ClaimsIdentity identity = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.UserName),
                    new Claim(ClaimTypes.Sid, user.Id)
                },
                    "ApplicationCookie"
                    );
                    Microsoft.Owin.IOwinContext ctx = Request.GetOwinContext();
                    Microsoft.Owin.Security.IAuthenticationManager authManager = ctx.Authentication;
                    authManager.SignIn(identity);
                    return Redirect(GetRedirectUrl(model.ReturnURL));
                }
                else
                {
                    ModelState.AddModelError("Invalid_Login", "Invalid login attempted, please check your username and password");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("Invalid_Login", "Invalid login attempted, please check your username: it was not found");
                return View(model);
            }
        }
        /// <summary>
        /// This private method will return a url to the User/Index page if the returnUrl parameter is 
        /// 
        ///     (1) : empty or 
        ///     
        ///     (2) : is not local to this site.
        /// 
        /// Case 2 can happen if they get reffered to our site from some other sitein the future.
        /// </summary>
        /// <param name="returnUrl">The url to take the client's browser after they authenticate into our web application.</param>
        /// <returns></returns>
        private string GetRedirectUrl(string returnUrl)
        {
            if(string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "User");
            }
            return returnUrl;
        }

        // GET: Auth/LogOff
        [Authorize]
        [HttpGet]
        public ActionResult LogOff()
        {
            // We need to remove all session variables between this web app and the client
            Session.RemoveAll();
            // ASP.NET stuff that was here to remove user authorization context.
            Microsoft.Owin.IOwinContext ctx = Request.GetOwinContext();
            Microsoft.Owin.Security.IAuthenticationManager authManager = ctx.Authentication;
            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("index", "Welcome");
        }

        // GET: Auth/Register
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Auth/Register
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register(Models.RegisterModel model)
        {
            //var s = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                // check if email is null or empty
                if(string.IsNullOrEmpty(model.Email))
                {
                    ModelState.AddModelError("empty_email", "Please enter an Email");
                }
                // check if email is a valid email
                else
                {
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([A-Za-z][\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    System.Text.RegularExpressions.Match match = regex.Match(model.Email);
                    if(!match.Success)
                    {
                        ModelState.AddModelError("invalid_email", "Provided email is invalid");
                    }
                }
                // check if password is empty or whitespace
                if(string.IsNullOrEmpty(model.Password))
                {
                    ModelState.AddModelError("empty_password", "Please enter a password");
                }
                // check if confirmation password is empty
                if(string.IsNullOrEmpty(model.ConfirmPassword))
                {
                    ModelState.AddModelError("empty_confirmpassword", "Please confirm your password");
                }
                // check if the password and password confirmation are the same
                if(!model.Password.Equals(model.ConfirmPassword))
                {
                    ModelState.AddModelError("missmatching_password", "Password was different from Confirmation");
                }
                // check if name is empty
                if(string.IsNullOrEmpty(model.Name))
                {
                    ModelState.AddModelError("empty_name", "Please enter an alias name");
                }
                if(ModelState.Any(x => x.Value.Errors.Count > 0))
                {
                    return View(model);
                }
                IdentityUser newUser = new IdentityUser { UserName = model.Email, PasswordHash = model.Password, Name = model.Name };
                var result = await appUserManager.CreateAsync(newUser, newUser.PasswordHash);
                if(result.Succeeded)
                {
                    ClaimsIdentity identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, newUser.Name),
                        new Claim(ClaimTypes.Email, newUser.UserName),
                        new Claim(ClaimTypes.Sid, newUser.Id)
                    },
                    "ApplicationCookie"
                    );
                    Microsoft.Owin.IOwinContext ctx = Request.GetOwinContext();
                    Microsoft.Owin.Security.IAuthenticationManager authManager = ctx.Authentication;
                    authManager.SignIn(identity);
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("Invalid_Login", "An error occured processing your registration.");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("Invalid_Login", "An error occured processing your request.");
                return View(model);
            }
        }
    }
}