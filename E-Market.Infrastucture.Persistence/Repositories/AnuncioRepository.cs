﻿using Microsoft.EntityFrameworkCore;
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

      
    }
}