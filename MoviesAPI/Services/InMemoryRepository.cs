using Microsoft.Extensions.Logging;
using MoviesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public class InMemoryRepository: IRepository
    {
        private List<Genre> _genres;
        private readonly ILogger<InMemoryRepository> logger;

        public InMemoryRepository(ILogger<InMemoryRepository> logger)
        {
            _genres = new List<Genre>()
            {
                new Genre(){Id = 1, Name = "Comedy"},
                new Genre(){Id = 2, Name = "Action"}
            };
            this.logger = logger;
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            logger.LogInformation("Executing GetAllGenres");
            await Task.Delay(1);
            return _genres;
        }

        public async Task<Genre> GetGenreById(int Id)
        {
            await Task.Delay(1);
            return _genres.FirstOrDefault(x => x.Id == Id);
        }

        public async Task AddGenre(Genre genre)
        {
            await Task.Delay(1);
            genre.Id = _genres.Max(x => x.Id) + 1;
            _genres.Add(genre);
        }

        public Task UpdateGenre(Genre genre)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteGenre(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Person>> GetAllPeople()
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetPersonById(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddPerson(Person person)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePerson(int id)
        {
            throw new NotImplementedException();
        }
    }
}
