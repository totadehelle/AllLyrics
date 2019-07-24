using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AllLyrics.Core;
using AllLyrics.Data;

namespace AllLyrics.Pages.Admin.Songs
{
    public class DetailsModel : PageModel
    {
        private readonly AllLyrics.Data.ApplicationContext _context;

        public DetailsModel(AllLyrics.Data.ApplicationContext context)
        {
            _context = context;
        }

        public Song Song { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Song = await _context.Songs
                .Include(s => s.Artist).FirstOrDefaultAsync(m => m.Id == id);

            if (Song == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
