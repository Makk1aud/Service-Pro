using Coursework.DataApp;
using Coursework.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Coursework.Repository.Extensions.FilterParameters;
using Coursework.Repository.Extensions;

namespace Coursework.Repository
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(CourseworkEntities context)
            : base(context)
        {
        }

        public void AddClient(Client client) =>
            Create(client);

        public void DeleteClient(Client client) =>
            Delete(client);

        public IEnumerable<Client> GetClients(bool trackChanges) =>
            FindAll(trackChanges)
            .ToList();

        public IEnumerable<Client> GetFilterClients(ClientParameters parameters, bool trackChanges) =>
            FindAll(trackChanges)
            .FindById(parameters.Id)
            .FindByPhone(parameters.PhoneNum)
            .ToList();
    }
}
