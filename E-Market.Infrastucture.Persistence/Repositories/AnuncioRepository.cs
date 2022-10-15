using Microsoft.EntityFrameworkCore;
using E_Market.Core.Application.Interfaces.Repositories;
using E_Market.Core.Domain.Entities;
using E_Market.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Market.Infrastructure.Persistence.Repository
{
    public class AnuncioRepository : GenericRepository<Anuncio>, IAnuncioRepository
    {
        private readonly ApplicationContext _dbContext;

        public AnuncioRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual async Task<List<Anuncio>> GetAllAsync()  //Se sobrescribe pero sigue cumpliendo su misma funcion SOLID
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            return await _dbContext.Set<Anuncio>()
                .Include(a => a.Fotos)
                .Include(a => a.User)
                .ToListAsync(); //Deferred execution
        }

        public virtual async Task<Anuncio> GetBywithRelationship(int id)
        {
            var temp = await _dbContext.Set<Anuncio>().Where(a => a.Id == id).Include(a => a.Fotos).Include(a => a.User).Include(a => a.Category).ToListAsync();
            return temp.First();

            //POR SI ACASO
            //  return _dbContext.Anuncios.Where(a => a.Id == id).Include(a => a.Fotos).Include(a => a.User).Include(a => a.Category).FirstOrDefault(); 
            //return a;
        }


    }
}
