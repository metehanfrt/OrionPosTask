using Microsoft.AspNetCore.Mvc;
using OrionPosTask.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrionPosTask.Models.ViewModel;
using OrionPosTask.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace OrionPosTask.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AuthController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Login()
        {
            return View(new User());
        }


        [HttpPost]
        public IActionResult Login([Bind("Email", "Password", "RememberMe")] User model)
        {
            if (model == null)
            {
                // Model null ise, gelen veri eksik veya uyumsuz
                // Giriş bilgileri hatalı
                ModelState.AddModelError("", "Geçersiz giriş bilgileri");
                return View(model);
            }


            if (ModelState.IsValid )
            {
                var user = _db.Users.FirstOrDefault(u => u.Email == model.Email);

                if (user != null && user.Password != null && user.Password == model.Password)
                {
                    // Giriş başarılı
                    HttpContext.Session.SetString("UserId", user.Id.ToString());

                    // Diğer işlemler (örneğin, hatırla beni özelliği)
                    if (model.RememberMe)
                    {
                        var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, "Admin") // Varsayılan olarak admin olduğunu varsayalım
                    // Burada diğer rolleri de ekleyebilirsiniz
                };

                        var authIdentity = new ClaimsIdentity(authClaims, "Auth");

                        var authPrincipal = new ClaimsPrincipal(new[] { authIdentity });

                        var authProps = new AuthenticationProperties
                        {
                            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30), // Cookie'nin süresi
                            IsPersistent = true // Cookie kalıcı mı?
                        };

                        HttpContext.SignInAsync("CookieAuth", authPrincipal, authProps);
                    }

                    return RedirectToAction("Index", "Directorys"); // Başka bir sayfaya yönlendirme yapabilirsiniz
                }
                else
                {
                    // Giriş bilgileri hatalı
                    ModelState.AddModelError("", "Geçersiz e-posta veya şifre");
                    return View(model);
                }
            }

            // Geçersiz model durumu, yani form doğrulama başarısız oldu
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserName, Email, Password")] User user)
        {
          
            if (ModelState.IsValid && ModelState.Count==3)
            {
                _db.Add(user);
                await _db.SaveChangesAsync();
                return RedirectToAction("Login", "Auth");
            }

           // return View(user); // Eğer ModelState geçerli değilse, kullanıcıya formu tekrar göster
            return View(new User()); // Eğer ModelState geçerli değilse, kullanıcıya formu tekrar göster

        }



        public IActionResult ForgotPassword()
        {
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("KullaniciAdi");
            HttpContext.Session.Remove("Rol");

            Response.Cookies.Delete("KullaniciAdi");
            Response.Cookies.Delete("Rol");

            return Redirect("/");
        }

    }

}
