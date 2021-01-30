using CleanArchitectureWebAPI.Domian.Interfaces.Base;
using CleanArchitectureWebAPI.Domian.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Domian.Interfaces
{
    public interface ISoapRepository : IBaseRepository<Soap>
    {
        // This is where we put the methods specific for that class/repository
    }
}
