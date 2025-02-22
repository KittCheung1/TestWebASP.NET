﻿using Microsoft.AspNetCore.Mvc;
using MovieWebASP.NET.DTO.Requests;
using MovieWebASP.NET.DTO.Responses;
using MovieWebASP.NET.Models;
using MovieWebASP.NET.Services;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace MovieWebASP.NET.Controllers
{
    [Route("api/v1/movies")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<IEnumerable<ReadMovieDTO>> GetAllMovies()
        {
            return _movieService.GetAllMoviesAsync();
        }

        /// <summary>
        /// Get a movie by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadMovieDTO>> GetMovie(int id)
        {
            var foundMovie = await _movieService.GetMovieAsync(id);

            if (foundMovie == null)
            {
                return NotFound();
            }

            return foundMovie;
        }

        /// <summary>
        /// Get all characters in a movie by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("characters/{id}")]
        public Task<IEnumerable<ReadCharacterDTO>> GetAllCharactersInMovie(int id)
        {
            return _movieService.GetAllCharacterInMovieAsync(id);
        }

        /// <summary>
        /// Update a movie by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateMovie"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, UpdateMovieDTO updateMovie)
        {
            bool movieExist = await _movieService.UpdateMovieAsync(updateMovie, id);

            if (!movieExist)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Update characters to a movie by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="characterIds"></param>
        /// <returns></returns>
        [HttpPut("characters/{id}")]
        public async Task<ActionResult> UpdateCharactersToMovie(int id, [FromBody] List<int> characterIds)
        {
            await _movieService.UpdateCharactersToMovieAsync(id, characterIds);
            return Ok();
        }

        /// <summary>
        /// Create a movie
        /// </summary>
        /// <param name="createMovie"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Movie>> CreateMovie(CreateMovieDTO createMovie)
        {
            var movie = await _movieService.CreateMovieAsync(createMovie);

            return CreatedAtAction(nameof(GetMovie), new { movie.Id }, movie);
        }

        /// <summary>
        /// Delete a movie by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            bool movieExists = await _movieService.DeleteMovieAsync(id);

            if (!movieExists)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
