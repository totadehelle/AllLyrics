using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AllLyrics.Pages
{
    public class IndexModel : PageModel
    {
        public char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        [BindProperty] public string SearchRequest { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("/SearchResults", new{ searchRequest = SearchRequest});
        }
    }
}
