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
    public class SongsModel : PageModel
    {
        private readonly ApplicationContext _context;

        [BindProperty(SupportsGet = true)] public int ArtistId { get; set; }
        [BindProperty(SupportsGet = true)] public int SongId { get; set; }

        public List<Song> Songs { get; set; }
        public Artist Artist { get; set; }
        public Song CurrentSong { get; set; }

        public SongsModel(ApplicationContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Artist = await _context.Artists.FindAsync(ArtistId);
            Songs = await _context.Songs.Where(s => s.ArtistId == ArtistId).ToListAsync();

            CurrentSong = Songs.Any(s => s.Id == SongId) ? Songs.Find(s => s.Id == SongId) : Songs.FirstOrDefault();
        }
    }
}