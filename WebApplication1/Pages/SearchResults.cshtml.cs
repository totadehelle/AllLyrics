using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllLyrics.Core;
using AllLyrics.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace AllLyrics.Pages
{
    //This model exists only for sending JSON for AJAX-requests on the main page (/Index).
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 86400)]
    public class SearchResultsModel : PageModel
    {
        private readonly ApplicationContext _context;

        private List<Artist> Artists { get; set; }
        private List<Song> Songs { get; set; }

        public SearchResultsModel(ApplicationContext context)
        {
            _context = context;
        }

        
        public JsonResult OnGet(string searchRequest)
        {
            Artists = _context.Artists.Where(a => a.Name.Contains(searchRequest)).ToList();
            Dictionary<string, string> ArtistsLinks = new Dictionary<string, string>();
            foreach (var artist in Artists)
            {
                ArtistsLinks.Add("/Songs/" + artist.Id, artist.Name);
            }
            
            Songs = _context.Songs.Where(a => a.Name.Contains(searchRequest)).ToList();
            Dictionary<string, string> SongsLinks = new Dictionary<string, string>();
            foreach (var song in Songs)
            {
                SongsLinks.Add("/Songs/" + song.ArtistId + "/" + song.Id, song.Name);
            }

            var results = new Dictionary<string, Dictionary<string, string>>();
            results.Add("Artists", ArtistsLinks);
            results.Add("Songs",   SongsLinks);
            return new JsonResult(results);
        }
    }
}