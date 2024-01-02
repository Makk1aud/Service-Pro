using Coursework.Contracts;
using Coursework.DataApp;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
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

        public RepositoryManager(CourseworkEntities context)
        {
            _context = context;
            _discountCardRepository = new Lazy<IGenericRepository<DiscountCard>>(() => new GenericRepository<DiscountCard>(context));
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(context));
            _clientRepository = new Lazy<IClientRepository>(() => new ClientRepository(context));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(context));
        }

        public IGenericRepository<DiscountCard> DiscountCard => _discountCardRepository.Value;

        public IProductRepository Product => _productRepository.Value;

        public IClientRepository Client => _clientRepository.Value;

        public IEmployeeRepository Employee => _employeeRepository.Value;

        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
                MessageBox.Show("Успешно",
                    "Уведомление",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Не удалось обновить данные, возможно пропущены поля для заполнения",
                    "Ошибка БД",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (DbException)
            {
                MessageBox.Show("Не удалось удалить данные, они привязаны с другим объектом",
                    "Ошибка БД",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка приложения",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
