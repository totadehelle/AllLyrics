using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllLyrics.Core;
using AllLyrics.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AllLyrics.Pages
{
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 86400)]
    public class ArtistsModel : PageModel
    {
        private readonly ApplicationContext _context;

        [BindProperty(SupportsGet = true)] public string Letter { get; set; } = "A";
        public List<Artist> Artists { get; set; }

        public ArtistsModel(ApplicationContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Artists = await _context.Artists.Where(a => a.Name.StartsWith(Letter)).ToListAsync();
        }
    }
}