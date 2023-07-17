
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using IconBetAuth.Domain;
using IconBetAuth.Domain.Models;
using IconBetAuth.Domino.Domain;

namespace IconBetAuth.Data.DB
{
    public class DataBase
    {
        private readonly IconBetAuthContext _iconBetAuthContext;
        private readonly IMapper _mapper;
        public DataBase(string stringConnDB, IMapper mapper)
        {
            _iconBetAuthContext = new IconBetAuthContext(stringConnDB);
            _mapper = mapper;
        }

        public Customer GetCustomer(string name)
        {
            return _iconBetAuthContext.Customer.FirstOrDefault(x => x.Name == name);
        }
        public Customer GetCustomer(int customerId)
        {
            return _iconBetAuthContext.Customer.FirstOrDefault(x => x.CustomerId == customerId);
        }
        public bool IsValidCustomer(int customerId)
        {
            return _iconBetAuthContext.Customer.Any(x => x.CustomerId == customerId && x.Active == true);
        }
        public bool SaveTransaction(Transaction transaction)
        {
            try
            {
                _iconBetAuthContext.Add(transaction);
                int result = _iconBetAuthContext.SaveChanges();
                return result == 1;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Hall GetHall(string parentHash, string currency)
        {
            var hall = _iconBetAuthContext.Hall.Where(x => x.ParentHash == parentHash && x.Currency == currency).FirstOrDefault();
            return hall;
        }

        public List<Hall> GetHalls()
        {
            var halls = _iconBetAuthContext.Hall.ToList();
            return halls;
        }
        public void SaveHall(Hall hallIncoming)
        {
            try
            {
                var hall = _iconBetAuthContext.Hall.Single(a => a.HallId == hallIncoming.HallId);
                if (hall != null)
                {
                    hall.AmountExChanged = hallIncoming.AmountExChanged;
                    _iconBetAuthContext.Update(hall);
                    _iconBetAuthContext.SaveChanges();
                }
                else
                {
                    _iconBetAuthContext.Add(hallIncoming);
                    _iconBetAuthContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}