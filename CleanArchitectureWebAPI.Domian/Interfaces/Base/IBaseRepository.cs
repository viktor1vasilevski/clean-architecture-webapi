using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Domian.Interfaces.Base
{
    public interface IBaseRepository<T> where T:class
    {
        /*
            This is where we put all the methods
            that are common for all entities.
         */
 
        IReadOnlyList<T> GetAll();
        T GetById(Guid id);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
