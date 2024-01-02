using Coursework.DataApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Repository.Extensions
{
    public static class ClientRepositoryExtensions
    {
        public static IQueryable<Client> FindByPhone(this IQueryable<Client> items, string phoneNumber)
        {
            if(String.IsNullOrWhiteSpace(phoneNumber))
                return items;
            items = items.Where(x => x.phone.StartsWith(phoneNumber, StringComparison.CurrentCultureIgnoreCase));
            return items;
        }

        public static IQueryable<Client> FindById(this IQueryable<Client> items, int? id)
        {
            if (id is null)
                return items;
            items = items.Where(x => x.client_id.Equals(id));

            //Поиск по id любой таблицы

            //var idProperty = items.GetType()
            //    .GetProperty(
            //        items
            //        .GetType()
            //        .GetProperties()
            //        .First().Name).Name;
            //items = items
            //    .Where(x => x.GetType().GetProperty(idProperty).Equals(id));
            return items;
        }
    } 
}
