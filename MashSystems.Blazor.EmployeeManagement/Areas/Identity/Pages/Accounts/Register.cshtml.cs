using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MashSystems.Blazor.EmployeeManagement.Areas.Identity.Pages.Accounts
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }
        public void OnGet()
        {
        }
    }
}
