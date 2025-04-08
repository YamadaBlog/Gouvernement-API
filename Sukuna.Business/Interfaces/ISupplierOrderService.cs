using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface ISupplierOrderService
{
    bool CreateSupplierOrder(Ressource supplierOrder);
    Ressource GetSupplierOrderById(int supplierOrderId);
    ICollection<Ressource> GetSupplierOrders();
    ICollection<Moderateur> GetOrderLinesBySupplierOrder(int cliendOrderId);
    bool UpdateSupplierOrder(Ressource supplierOrder);
    bool DeleteSupplierOrder(Ressource supplierOrder);
    bool SupplierOrderExists(RessourceResource supplierOrderCreate);
    bool SupplierOrderExistsById(int supplierOrderId);

    bool Save();
}
