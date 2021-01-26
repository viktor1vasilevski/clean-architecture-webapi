using CleanArchitectureWebAPI.Domian.Interfaces;
using CleanArchitectureWebAPI.Domian.Models;
using CleanArchitectureWebAPI.Infrastructure.Data.Context;
using CleanArchitectureWebAPI.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Infrastructure.Data.Repositories
{
    public class SoapRepository : BaseRepository<Soap>, ISoapRepository
    {
        public SoapRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
