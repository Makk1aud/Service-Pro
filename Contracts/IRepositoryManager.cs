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
        IProductRepository Product { get;}
        IClientRepository Client { get;}
        IEmployeeRepository Employee { get;}
        IMaterialRepository Material { get;}
        IGenericRepository<ProductType> ProductType { get;}
        IGenericRepository<ProductStatus> ProductStatus{ get;}
        IGenericRepository<Expenditure> Expenditure { get; }
        IGenericRepository<MaterialType> MaterialType { get; }
        IGenericRepository<Position> Position { get; }
        IGenericRepository<Manufacturer> Manufacturer { get; }
        Task SaveAsync();
    }
}
