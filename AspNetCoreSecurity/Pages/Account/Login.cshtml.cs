using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreSecurity.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Verify the credential
            if (!string.IsNullOrWhiteSpace(Credential.UserName) && Credential.Password == Credential.UserName)
            {
                // Creating the security context
                var claims = new List<Claim> {
                    new ("sub", "123"),
                    new ("name", "suhut"),
                    new ("role", "Admin")
                };
                var ci = new ClaimsIdentity(claims, "pwd", "name", "role");
                var cp = new ClaimsPrincipal(ci);

                // await HttpContext.SignInAsync("demo", cp);
                await HttpContext.SignInAsync(cp);

                // return Redirect("xxx");
                // return LocalRedirect("xxx");
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }

    public class Credential
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
