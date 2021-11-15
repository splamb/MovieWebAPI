using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieWebAPI.Data;
using System.Data.SqlClient;
using System.Data;

namespace MovieWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private readonly MovieContext _context;

        public MovieController(MovieContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Movie.ToListAsync());
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Movie movie)
        {
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(int Id, string Title, string Genre, int Runtime)
        {
            foreach (Movie movie in _context.Movie.ToList())
            {
                if (movie.Id == Id)
                {
                    movie.Title = Title;
                    movie.Genre = Genre;
                    movie.Runtime = Runtime;
                }
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var deleteMovie = _context.Movie.FirstOrDefault(m => m.Id == Id);
            if (deleteMovie != null)
            {
                _context.Movie.Remove(deleteMovie);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
