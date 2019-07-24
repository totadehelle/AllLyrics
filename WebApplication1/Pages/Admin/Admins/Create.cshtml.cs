using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllLyrics.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AllLyrics.Core;
using AllLyrics.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AllLyrics.Pages.Admin.Admins
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly Encryptor _encryption;
        public string Message { get; set; }

        public CreateModel(ApplicationContext context, IOptions<Constants> config)
        {
            _context = context;
            _encryption = new Encryptor(config);
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Core.Admin Admin { get; set; }

        public async Task<IActionResult> OnPostAsync(IFormCollection CoverImage)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (AdminExists(Admin.Login))
            {
                Message = "User with this login already exists, please choose other login.";
                return Page();
            }

            Message = null;

            Admin.Password = _encryption.HashPassword(Admin.Password);
            Admin.LastChanged = DateTime.UtcNow;

            await _context.Admins.AddAsync(Admin);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool AdminExists(string login)
        {
            return _context.Admins.AsNoTracking().Any(e => e.Login == login);
        }
    }
}