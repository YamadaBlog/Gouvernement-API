using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface ISupplierService
{
    bool CreateSupplier(Commentaire supplier);
    Commentaire GetSupplierById(int supplierId);
    ICollection<Commentaire> GetSuppliers();
    ICollection<Utilisateur> GetArticlesBySupplier(int supplierId);
    bool UpdateSupplier(Commentaire supplier);
    bool DeleteSupplier(Commentaire supplier);
    Commentaire SupplierExists(InteractionResource supplierCreate);
    bool SupplierExistsById(int supplierId);

    bool Save();
}
