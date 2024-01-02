using Coursework.DataApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Contracts
{
    public interface IRepositoryManager
    {
        IGenericRepository<DiscountCard> DiscountCard { get;}
        //Добавить больше полей с generic типами таблиц
        IProductRepository Product { get;}
        IClientRepository Client { get;}
        IEmployeeRepository Employee { get;}
        Task Save();
    }
}
