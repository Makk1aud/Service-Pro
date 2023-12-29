using Coursework.DataApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Contracts
{
    public interface IClientRepository
    {
        List<Client> GetClients();
        List<Client> GetClientsByPhone(string phoneNumber);
        void AddClient(Client client);
        void DeleteClient(Client client);
    }
}
