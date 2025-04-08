using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.DataAccess.Data;


namespace Sukuna.Service.Services;

public class ClientOrderService : IClientOrderService
{
    private readonly DataContext _context;

    public ClientOrderService(DataContext context)
    {
        _context = context;
    }

    public bool CreateClientOrder(Participation clientOrder)
    {
        _context.Add(clientOrder);

        return Save();
    }

    public ICollection<Participation> GetClientOrders()
    {
        return _context.ClientOrders.OrderBy(p => p.ID).ToList();
    }
    public Participation GetClientOrderById(int clientOrderId)
    {
        return _context.ClientOrders.Where(c => c.ID == clientOrderId).FirstOrDefault();
    }

    public ICollection<Moderateur> GetOrderLinesByClientOrder(int cliendOrderId)
    {
        return _context.OrderLines.Where(r => r.ClientOrder.ID == cliendOrderId).ToList();
    }

    public bool UpdateClientOrder(Participation clientOrder)
    {
        _context.Update(clientOrder);
        return Save();
    }
    public bool DeleteClientOrder(Participation clientOrder)
    {
        _context.Remove(clientOrder);
        return Save();
    }

    public bool ClientOrderExists(ParticipationResource clientOrderCreate)
    {
        if (_context.ClientOrders.Any(r => r.ID == clientOrderCreate.ID))
        {
            return true;
        }
        else
        {
            return (!_context.ClientOrders.Any(r => r.ID == clientOrderCreate.ID)) && (!_context.Clients.Any(r => r.ID == clientOrderCreate.ClientID));
        }
    }

    public bool ClientOrderExistsById(int clientOrderId)
    {
        return _context.ClientOrders.Any(r => r.ID == clientOrderId);

    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}
