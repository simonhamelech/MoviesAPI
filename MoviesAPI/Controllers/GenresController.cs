using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Filters;
using MoviesAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController: ControllerBase
    {
        private readonly IRepository repository;
        private readonly ILogger<GenresController> logger;
        private readonly IMapper mapper;

        //private readonly ApplicationDbContext context;

        public GenresController(IRepository repository, ILogger<GenresController> logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;

            //context = ctx;
        }

        [HttpGet] // api/genres
        [HttpGet("list")] // api/genres/list
        [HttpGet("/allgenres")] // allgenres
        //[ResponseCache(Duration = 60)]
        //[ServiceFilter(typeof(MyActionFilter))]
        public async Task<ActionResult<List<GenreDTO>>> Get()
        {
            logger.LogInformation("Getting all the genres");

            var allGenres = await repository.GetAllGenres();
            var genresDTO = mapper.Map<List<GenreDTO>>(allGenres);
            return genresDTO;
            //return await context.Genres.ToListAsync();
        }

        [HttpGet("{Id:int}", Name = "getGenre")] // api/genres/example
        public async Task<ActionResult<GenreDTO>> Get(int Id)
        {
            var genre = await repository.GetGenreById(Id);

            if (genre == null)
            {
                return NotFound();
            }

            //return Ok(2);
            //return "felipe";
            return Ok(mapper.Map<GenreDTO>(genre));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenreCreationDTO genreCreationDTO)
        {
            //context.Add(genre);
            //await context.SaveChangesAsync();
            var genre = mapper.Map<Genre>(genreCreationDTO);
            await repository.AddGenre(genre);
            var genreDTO = mapper.Map<GenreDTO>(genre);
            return new CreatedAtRouteResult("getGenre", new { genreDTO.Id }, genreDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] GenreCreationDTO genreCreation)
        {
            var genre = mapper.Map<Genre>(genreCreation);
            genre.Id = id;
            await repository.UpdateGenre(genre);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await repository.DeleteGenre(id);
            if (!exists)
                return NotFound();
            return NoContent();
        }
    }
}
