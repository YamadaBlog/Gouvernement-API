using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface ITvaTypeService
{
    bool CreateTvaType(Badge tvaType);
    Badge GetTvaTypeById(int tvaTypeId);
    ICollection<Badge> GetTvaTypes();
    bool UpdateTvaType(Badge tvaType);
    bool DeleteTvaType(Badge tvaType);
    Badge TvaTypeExists(BadgeResource tvaTypeCreate);
    bool TvaTypeExistsById(int tvaTypeId);

    bool Save();
}
