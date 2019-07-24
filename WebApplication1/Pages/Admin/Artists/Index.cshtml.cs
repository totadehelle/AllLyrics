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
    public class IndexModel : PageModel
    {
        private readonly AllLyrics.Data.ApplicationContext _context;

        public IndexModel(AllLyrics.Data.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Artist> Artist { get;set; }

        public async Task OnGetAsync()
        {
            Artist = await _context.Artists.ToListAsync();
        }
    }
}
