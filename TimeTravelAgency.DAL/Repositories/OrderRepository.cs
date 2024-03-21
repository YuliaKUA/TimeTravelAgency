﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.DAL.Interfaces;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Enum;

namespace TimeTravelAgency.DAL.Repositories
{
    public class OrderRepository : IBaseRepository<Order>
    {
        private readonly TimeTravelAgencyContext _db;

        public OrderRepository(TimeTravelAgencyContext db)
        {
            _db = db;
        }
        public async Task Create(Order entity)
        {
            _db.Orders.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Order entity)
        {
            _db.Orders.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Order> GetAll()
        {
            return _db.Orders;
        }

        public async Task<Order> GetById(int id)
        {
            return await _db.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Order> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> SelectAll()
        {
            return _db.Orders.ToListAsync();
        }

        public async Task<IQueryable<ExtendedOrder>> SelectExtendedOrder(int userId, StatusOrder status)
        {
            var extendedOrders = await (from o in _db.Orders
                                        where o.UserId == userId && o.Status == (StatusOrder)Convert.ToInt32(status)
                                        join t in _db.Tours on o.TourId equals t.Id
                                        select new ExtendedOrder
                                        {
                                            Id = o.Id,
                                            TourId = t.Id,
                                            UserId = o.UserId,
                                            DateCreate = o.DateCreate,
                                            Status = o.Status,
                                            Title = t.Title,
                                            DateStart = t.DateStart,
                                            DateEnd = t.DateEnd,
                                            Descriptions = t.Descriptions,
                                            Number = o.Number,
                                        }).ToListAsync();

            return extendedOrders.AsQueryable();
        }

        public async Task<IEnumerable<Order>> SelectOrder(int userId, StatusOrder status)
        {
            var extendedOrders = await (from o in _db.Orders
                                        where o.UserId == userId && o.Status == status
                                        select new Order
                                        {
                                            Id = o.Id,
                                            TourId = o.TourId,
                                            UserId = o.UserId,
                                            DateCreate = o.DateCreate,
                                            Status = o.Status,
                                            Number = o.Number,
                                        }).ToListAsync();

            return extendedOrders.AsEnumerable();
        }

        public async Task<Order> Update(Order entity)
        {
            _db.Orders.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateRange(IEnumerable<Order> values)
        {
            _db.UpdateRange(values);
            await _db.SaveChangesAsync();
        }
    }
}