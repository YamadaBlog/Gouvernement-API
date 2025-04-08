using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface IClientOrderService
{
    bool CreateClientOrder(Participation clientOrder);
    Participation GetClientOrderById(int clientOrderId);
    ICollection<Participation> GetClientOrders();
    ICollection<Moderateur> GetOrderLinesByClientOrder(int cliendOrderId);
    bool UpdateClientOrder(Participation clientOrder);
    bool DeleteClientOrder(Participation clientOrder);
    bool ClientOrderExists(ParticipationResource clientOrderCreate);
    bool ClientOrderExistsById(int clientOrderId);

    bool Save();
}
