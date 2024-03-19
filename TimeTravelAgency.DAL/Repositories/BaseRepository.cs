using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.DAL.Interfaces;
using TimeTravelAgency.Domain.Response;

namespace TimeTravelAgency.DAL.Repositories
{
    internal class BaseRepository<T> : IBaseRepository<T>
    {
        private readonly TimeTravelAgencyContext _db;

        public BaseRepository(TimeTravelAgencyContext db)
        {
            _db = db;
        }

        public Task Create(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> SelectAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
