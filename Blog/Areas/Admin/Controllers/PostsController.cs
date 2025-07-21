using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PostsController : Controller
    {
        private readonly BlogDbContext _db;
        public PostsController(BlogDbContext db) => _db = db;

        public async Task<IActionResult> Index() =>
            View(await _db.Posts.Include(p => p.PostTags).ToListAsync());

        public async Task<IActionResult> Create(Post vm, IFormFile? cover)
        {
            if (!ModelState.IsValid) return View(vm);

            // save image
            if (cover is { Length: > 0 })
            {
                var fileName = Path.GetRandomFileName() + Path.GetExtension(cover.FileName);
                var path = Path.Combine("wwwroot/uploads", fileName);
                await using var stream = System.IO.File.Create(path);
                await cover.CopyToAsync(stream);
                vm.CoverImagePath = $"/uploads/{fileName}";
            }

            _db.Add(vm);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
