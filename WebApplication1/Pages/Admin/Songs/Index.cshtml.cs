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
    public class IndexModel : PageModel
    {
        private readonly AllLyrics.Data.ApplicationContext _context;

        public IndexModel(AllLyrics.Data.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Song> Song { get;set; }

        public async Task OnGetAsync()
        {
            Song = await _context.Songs
                .Include(s => s.Artist).ToListAsync();
        }
    }
}
