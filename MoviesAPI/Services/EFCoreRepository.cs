using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public class EFCoreRepository : IRepository
    {
        private readonly ILogger<EFCoreRepository> logger;
        private readonly ApplicationDbContext context;
        public EFCoreRepository(ILogger<EFCoreRepository> lgr, ApplicationDbContext ctx)
        {
            logger = lgr;
            context = ctx;
        }

        public async Task AddGenre(Genre genre)
        {
            context.Genres.Add(genre);
            await context.SaveChangesAsync();
        }

        public async Task AddPerson(Person person)
        {
            context.People.Add(person);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteGenre(int id)
        {
            var exists = await context.Genres.AnyAsync(x => x.Id == id);
            if (exists)
            {
                context.Genres.Remove(new Genre() { Id = id });
                await context.SaveChangesAsync();
            }
            return exists;
        }

        public async Task<bool> DeletePerson(int id)
        {
            var exists = await context.People.AnyAsync(x => x.Id == id);
            if (exists)
            {
                context.People.Remove(new Person() { Id = id });
                await context.SaveChangesAsync();
            }
            return exists;
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            return await context.Genres.AsNoTracking().ToListAsync();
        }

        public async Task<List<Person>> GetAllPeople()
        {
            return await context.People.AsNoTracking().ToListAsync();
        }

        public async Task<Genre> GetGenreById(int id)
        {
            return await context.Genres.FirstOrDefaultAsync(x => x.Id == id);
       }

        public async Task<Person> GetPersonById(int id)
        {
            return await context.People.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateGenre(Genre genre)
        {
            context.Entry(genre).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task UpdatePerson(Person person)
        {
            context.Entry(person).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
