using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public interface IRepository
    {
        Task AddGenre(Genre genre);
        Task<List<Genre>> GetAllGenres();
        Task<Genre> GetGenreById(int Id);
        Task UpdateGenre(Genre genre);
        Task<bool> DeleteGenre(int id);
        Task<List<Person>> GetAllPeople();
        Task<Person> GetPersonById(int id);
        Task AddPerson(Person person);
        Task UpdatePerson(Person person);
        Task<bool> DeletePerson(int id);
    }
}
