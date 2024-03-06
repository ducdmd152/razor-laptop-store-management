using MyShopManagementService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyShopRazorPages.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly IUserService userService;

        public IndexModel(IUserService userService)
        {
            this.userService = userService;
        }

        [BindProperty]
        public Credential Credential { get; set; } = default!;
        public string ErrorString { get; set; } = "";

        public void OnGet(int? error, string? user)
        {
            if (error != null)
            {
                if (error == 1)
                {
                    ErrorString = "Your account is unenable, can not login the system!";
                }
                if (error == 2)
                {
                    ErrorString = "Wrong email or password, please try again!";
                }
                if (error == 3)
                {
                    ErrorString = "Something wrong to login!";
                }
            }
            Credential = new Credential() { User = user };
        }
      
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Credential != null)
            {
                string user = Credential.User;
                string pwd = Credential.Password;

                var account = userService.Get(user);
                if (account != null && account.Password == pwd)
                {
                    if (account.Enabled)
                    {
						HttpContext.Session.SetString("CREDENTIAL", 
                            JsonConvert.SerializeObject(account, new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            })
                         );
						HttpContext.Session.SetString("ROLE", account.Role.Name.ToUpper());
						return RedirectToPage("/Index");
					}
                    else
                    {
						return RedirectToPage("/Login/Index", new { error = 1, user = Credential.User });
					}
                }
                else
                {
                    return RedirectToPage("/Login/Index", new { error = 2, user = Credential.User });
                }
            }
            return RedirectToPage("/Login/Index", new { error = 3, user = Credential.User });
        }
    }
    public class Credential
    {
        [Required]
        public string User { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
