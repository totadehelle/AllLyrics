using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AllLyrics.Core;
using AllLyrics.Data;

namespace AllLyrics.Pages.Admin.Artists
{
    public class DetailsModel : PageModel
    {
        private readonly AllLyrics.Data.ApplicationContext _context;

        public DetailsModel(AllLyrics.Data.ApplicationContext context)
        {
            _context = context;
        }

        public Artist Artist { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Artist = await _context.Artists.FirstOrDefaultAsync(m => m.Id == id);

            if (Artist == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
