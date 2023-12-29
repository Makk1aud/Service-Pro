using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coursework.DataApp
{
    public static class ModelClass
    {
        private static CourseworkEntities _context = new CourseworkEntities();
        
        public static CourseworkEntities GetContext() => _context;

        public static void SaveDatabase()
        {
            try
            {
                _context.SaveChangesAsync();
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
            catch(DbException)
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
