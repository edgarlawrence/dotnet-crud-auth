using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using simple_crud_and_authentication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace simple_crud_and_authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _movieContext;
        public MoviesController(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }

        // get all data
        [HttpGet]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            if (_movieContext.Movies == null)
            {
                return NotFound();
            }

            var getAllMovies = await _movieContext.Movies.ToListAsync();

            return Ok(getAllMovies);
        }

        // get data by id
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            if (_movieContext.Movies == null)
            {
                return NotFound();
            }

            var movie = await _movieContext.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // add data
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _movieContext.Movies.Add(movie);
            await _movieContext.SaveChangesAsync();

            var postMovie = CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);

            return Ok(postMovie);
        }

        /// edit data
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<Movie>> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _movieContext.Entry(movie).State = EntityState.Modified;

            try
            {
                await _movieContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExist(id))
                {
                    return NotFound();
                } else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            if (_movieContext.Movies == null)
            {
                return NotFound();
            }

            var movie = await _movieContext.Movies.FindAsync(id);

            if(movie == null)
            {
                return NotFound();
            }

            _movieContext.Movies.Remove(movie);
            await _movieContext.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExist(int id)
        {
            return (_movieContext.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
