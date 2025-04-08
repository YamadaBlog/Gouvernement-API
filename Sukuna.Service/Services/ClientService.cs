using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.DataAccess.Data;


namespace Sukuna.Service.Services;

public class ClientService : IModerateurService
{
    private readonly DataContext _context;

    public ClientService(DataContext context)
    {
        _context = context;
    }

    public bool CreateClient(Evenement client)
    {
        _context.Add(client);

        return Save();
    }

    public ICollection<Evenement> GetClients()
    {
        return _context.Clients.OrderBy(p => p.ID).ToList();
    }
    public Evenement GetClientById(int clientId)
    {
        return _context.Clients.Where(c => c.ID == clientId).FirstOrDefault();
    }

    public Evenement GetAuthauthClient(string clientEmail, string clientMpd) {
        return _context.Clients.Where(c => c.Email == clientEmail && c.MotDePasseHashe == clientMpd).FirstOrDefault();
    }

    public ICollection<Participation> GetClientOrdersByClient(int clientId)
    {
        return _context.ClientOrders.Where(r => r.Client.ID == clientId).ToList();
    }

    public bool UpdateClient(Evenement client)
    {
        _context.Update(client);
        return Save();
    }
    public bool DeleteClient(Evenement client)
    {
        _context.Remove(client);
        return Save();
    }

    public bool ClientExists(ModerateurResource clientCreate)
    {
        return _context.Clients.Any(r => r.ID == clientCreate.ID);
    }

    public bool ClientExistsById(int clientId)
    {
        return _context.Clients.Any(r => r.ID == clientId);

    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}
