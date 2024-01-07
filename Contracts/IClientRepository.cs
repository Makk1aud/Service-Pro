using Coursework.DataApp;
using Coursework.Repository.Extensions.FilterParameters;
using Coursework.Repository.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// добавить в сервис слой отдельные свойства с уже заранне указаными типами в генерик репозитории
namespace Coursework.Contracts
{
    public interface IClientRepository
    {
        PagedList<Client> GetClients(bool trackChanges);
        PagedList<Client> GetClients(ClientParameters parameters, bool trackChanges);
        void CreateClient(Client client);
        void DeleteClient(Client client);
    }
}
