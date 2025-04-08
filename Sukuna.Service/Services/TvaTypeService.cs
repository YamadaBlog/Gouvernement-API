using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.DataAccess.Data;


namespace Sukuna.Service.Services;

public class TvaTypeService : ITvaTypeService
{
    private readonly DataContext _context;

    public TvaTypeService(DataContext context)
    {
        _context = context;
    }

    public bool CreateTvaType(Badge tvaType)
    {
        _context.Add(tvaType);

        return Save();
    }

    public ICollection<Badge> GetTvaTypes()
    {
        return _context.TvaTypes.OrderBy(p => p.ID).ToList();
    }
    public Badge GetTvaTypeById(int tvaTypeId)
    {
        return _context.TvaTypes.Where(c => c.ID == tvaTypeId).FirstOrDefault();
    }

    public bool UpdateTvaType(Badge tvaType)
    {
        _context.Update(tvaType);
        return Save();
    }
    public bool DeleteTvaType(Badge tvaType)
    {
        _context.Remove(tvaType);
        return Save();
    }

    public Badge TvaTypeExists(BadgeResource tvaTypeCreate)
    {
        return GetTvaTypes().Where(c => c.Nom.Trim().ToUpper() == tvaTypeCreate.Nom.TrimEnd().ToUpper())
            .FirstOrDefault();
    }

    public bool TvaTypeExistsById(int tvaTypeId)
    {
        return _context.TvaTypes.Any(r => r.ID == tvaTypeId);

    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}
