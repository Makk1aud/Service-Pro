using Coursework.DataApp;
using Coursework.Repository.Extensions.FilterParameters;
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
        IEnumerable<Client> GetClients(bool trackChanges);
        IEnumerable<Client> GetFilterClients(ClientParameters parameters, bool trackChanges);
        void AddClient(Client client);
        void DeleteClient(Client client);
    }
}
