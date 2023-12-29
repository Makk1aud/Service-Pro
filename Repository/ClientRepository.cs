using Coursework.DataApp;
using Coursework.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coursework.Repository
{
    public class ClientRepository : IClientRepository
    {
        private static CourseworkEntities _context;

        static ClientRepository()
        {
            _context = ModelClass.GetContext();
        }

        public void AddClient(Client client)
        {
            _context.Client.Add(client);
            ModelClass.SaveDatabase();
        }

        public void DeleteClient(Client client)
        {

            _context.Client.Remove(client);
            ModelClass.SaveDatabase();

        }

        public List<Client> GetClients()
        {
            return _context.Client.ToList();
        }

        public List<Client> GetClientsByPhone(string phoneNumber)
        {
            return _context.Client.Where(x => x.phone.StartsWith(phoneNumber)).ToList();
        }
    }
}
