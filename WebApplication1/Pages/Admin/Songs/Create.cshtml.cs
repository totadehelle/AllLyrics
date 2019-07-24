using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AllLyrics.Core;
using AllLyrics.Data;
using Microsoft.EntityFrameworkCore;

namespace AllLyrics.Pages.Admin.Songs
{
    public class CreateModel : PageModel
    {
        private readonly AllLyrics.Data.ApplicationContext _context;
        public List<SelectListItem> ArtistsList => _context.Artists.AsNoTracking()
            .Select(artist => new SelectListItem
            {
                Value = artist.Id.ToString(),
                Text = artist.Name
            }).ToList();


        public CreateModel(AllLyrics.Data.ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Song Song { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Song.Text = Song.Text.Replace(System.Environment.NewLine, "<br>");
            _context.Songs.Add(Song);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}