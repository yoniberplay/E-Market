using E_Market.Core.Application.Interfaces.Repositories;
using E_Market.Core.Domain.Entities;
using E_Market.Infrastructure.Persistence.Contexts;
using E_Market.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Infrastucture.Persistence.Repositories
{
    public class FotoRepository : GenericRepository<Fotos>, IFotoRepository
    {
        private readonly ApplicationContext _dbContext;

        public FotoRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


    }
}
