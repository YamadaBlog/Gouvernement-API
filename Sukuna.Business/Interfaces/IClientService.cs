using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface IClientService
{
    bool CreateClient(Evenement client);
    Evenement GetClientById(int clientId);
    Evenement GetAuthauthClient(string clientEmail, string clientMpd);
    ICollection<Evenement> GetClients();
    ICollection<Participation> GetClientOrdersByClient(int clientId);
    bool UpdateClient(Evenement client);
    bool DeleteClient(Evenement client);
    bool ClientExists(ModerateurResource clientCreate);
    bool ClientExistsById(int clientId);

    bool Save();
}
