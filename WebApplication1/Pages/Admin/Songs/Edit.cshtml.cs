﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AllLyrics.Core;
using AllLyrics.Data;

namespace AllLyrics.Pages.Admin.Songs
{
    public class EditModel : PageModel
    {
        private readonly AllLyrics.Data.ApplicationContext _context;

        public EditModel(AllLyrics.Data.ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(Song.Id))
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

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }
    }
}
