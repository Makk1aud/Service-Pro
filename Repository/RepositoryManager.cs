using Coursework.Contracts;
using Coursework.DataApp;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coursework.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly CourseworkEntities _context;
        private readonly Lazy<IGenericRepository<DiscountCard>> _discountCardRepository;
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<IClientRepository> _clientRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IMaterialRepository> _materialRepository;
        private readonly Lazy<IGenericRepository<ProductType>> _productTypeRepository;
        private readonly Lazy<IGenericRepository<ProductStatus>> _productStatusRepository;
        private readonly Lazy<IGenericRepository<Expenditure>> _expernditureRepository;
        private readonly Lazy<IGenericRepository<MaterialType>> _materialTypeRepository;
        private readonly Lazy<IGenericRepository<Position>> _positionRepository;
        private readonly Lazy<IGenericRepository<Manufacturer>> _manufacturerRepository;
        

        public RepositoryManager(CourseworkEntities context)
        {
            _context = context;
            _discountCardRepository = new Lazy<IGenericRepository<DiscountCard>>(() => new GenericRepository<DiscountCard>(context));
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(context));
            _clientRepository = new Lazy<IClientRepository>(() => new ClientRepository(context));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(context));
            _materialRepository = new Lazy<IMaterialRepository>(() => new MaterialRepository(context));
            _productTypeRepository = new Lazy<IGenericRepository<ProductType>>(() => new GenericRepository<ProductType>(context));
            _productStatusRepository = new Lazy<IGenericRepository<ProductStatus>>(() => new GenericRepository<ProductStatus>(context));
            _expernditureRepository = new Lazy<IGenericRepository<Expenditure>>(() => new GenericRepository<Expenditure>(context));
            _materialTypeRepository = new Lazy<IGenericRepository<MaterialType>>(() => new GenericRepository<MaterialType>(context));
            _positionRepository = new Lazy<IGenericRepository<Position>>(() => new GenericRepository<Position>(context));
            _manufacturerRepository = new Lazy<IGenericRepository<Manufacturer>>(() => new GenericRepository<Manufacturer>(context));
        }

        public IGenericRepository<DiscountCard> DiscountCard => _discountCardRepository.Value;

        public IProductRepository Product => _productRepository.Value;

        public IClientRepository Client => _clientRepository.Value;

        public IEmployeeRepository Employee => _employeeRepository.Value;

        public IGenericRepository<ProductType> ProductType => _productTypeRepository.Value;

        public IGenericRepository<ProductStatus> ProductStatus => _productStatusRepository.Value;

        public IGenericRepository<Expenditure> Expenditure => _expernditureRepository.Value;

        public IGenericRepository<MaterialType> MaterialType => _materialTypeRepository.Value;

        public IMaterialRepository Material => _materialRepository.Value;

        public IGenericRepository<Position> Position => _positionRepository.Value;

        public IGenericRepository<Manufacturer> Manufacturer => _manufacturerRepository.Value;

        private void ClearDbExceptions()
        {
            foreach (var dbEntityEntry in _context.ChangeTracker.Entries().ToArray())
            {
                if (dbEntityEntry.Entity != null)
                    dbEntityEntry.State = EntityState.Detached;
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                MessageBox.Show("Успешно",
                    "Уведомление",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Не удалось обновить данные, возможно пропущены поля для заполнения",
                    "Ошибка БД",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                ClearDbExceptions();
            }
            catch (DbException)
            {
                MessageBox.Show("Не удалось удалить данные, они привязаны с другим объектом",
                    "Ошибка БД",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                ClearDbExceptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка приложения",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                ClearDbExceptions();
            }
        }
    }
}
