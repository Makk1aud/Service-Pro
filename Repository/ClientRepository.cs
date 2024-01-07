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
using Coursework.Repository.Pagination;
using System.Security.Cryptography;

namespace Coursework.Repository
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(CourseworkEntities context)
            : base(context)
        {
        }

        public void CreateClient(Client client) =>
            Create(client);

        public void DeleteClient(Client client) =>
            Delete(client);

        public PagedList<Client> GetClients(bool trackChanges)
        {
            var clientParameters = new ClientParameters();
            var items = FindAll(trackChanges)
                .OrderBy(x => x.firstname)
                .Skip((clientParameters.PageNumber - 1) * clientParameters.PageSize)
                .Take(clientParameters.PageSize)
                .ToList();

            var count = FindAll(trackChanges).Count();
            return new PagedList<Client>(items, count, clientParameters.PageNumber, clientParameters.PageSize);
        }

        public PagedList<Client> GetClients(ClientParameters clientParameters, bool trackChanges)
        {
            var items = FindAll(trackChanges)
                .FindById(clientParameters.Id)
                .FindByPhone(clientParameters.PhoneNum)
                .OrderBy(x => x.firstname)
                .Skip((clientParameters.PageNumber - 1) * clientParameters.PageSize)
                .Take(clientParameters.PageSize)
                .ToList();

            var count = FindAll(trackChanges).Count();
            return new PagedList<Client>(items, count, clientParameters.PageNumber, clientParameters.PageSize);
        }            
    }
}
