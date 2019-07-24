using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllLyrics.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AllLyrics.Core;
using AllLyrics.Data;
using Microsoft.Extensions.Options;

namespace AllLyrics.Pages.Admin.Admins
{
    public class EditModel : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly Encryptor _encryption;

        public EditModel(ApplicationContext context, IOptions<Constants> config)
        {
            _context = context;
            _encryption = new Encryptor(config);
        }

        [BindProperty]
        public Core.Admin Admin { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Admin = await _context.Admins.FirstOrDefaultAsync(m => m.Id == id);

            if (Admin == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Admin.Password = _encryption.HashPassword(Admin.Password);
            Admin.LastChanged = DateTime.UtcNow;

            _context.Attach(Admin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(Admin.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.Id == id);
        }
    }
}
