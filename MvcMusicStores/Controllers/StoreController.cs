using Microsoft.AspNetCore.Mvc;
using MvcMusicStores.Data;
using MvcMusicStores.Models;
using Microsoft.EntityFrameworkCore;


namespace MvcMusicStores.Controllers
{
    public class StoreController : Controller
    {
        private readonly MvcMusicStoresContext _context;
        public StoreController(MvcMusicStoresContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var genres = _context.Genre.ToList();

            return View(genres);
        }

        public IActionResult Browse(int genre)
        {
            //var idGenre = _context.Genre.Where(x => x.Name == genre).FirstOrDefault();

            var listAlbum = _context.Album.Where(g => g.Genre.GenreId == genre);

            //var genreModel = new Genre { Name = genre };
            return View(listAlbum);
        }

        public IActionResult Details(int id)
        {
            var album = _context.Album.Find(id);

            return View(album);
        }
    }
}
