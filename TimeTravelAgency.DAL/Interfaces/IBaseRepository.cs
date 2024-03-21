using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Enum;

namespace TimeTravelAgency.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task Create(T entity);
        //Task<T> GetById(int id);
        //Task<T> GetByName(string name);
        Task<List<T>> SelectAll();
        Task<T> Update(T entity);
        Task Delete(T entity);
        Task UpdateRange(IEnumerable<T> values);

        IQueryable<T> GetAll();

    }
}
