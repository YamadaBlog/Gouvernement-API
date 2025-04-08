using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface IOrderLineService
{
    bool CreateOrderLine(Moderateur orderLine);

    Moderateur GetOrderLineById(int orderLineId);
    ICollection<Moderateur> GetOrderLines();
    ICollection<Moderateur> GetOrderLinesOfAClientOrder(int clientOrderId);
    ICollection<Moderateur> GetOrderLinesOfASupplierOrder(int supplierOrderId);
    ICollection<Moderateur> GetOrderLinesOfAArticle(int articleId);
    bool UpdateOrderLine(Moderateur orderLine);
    bool DeleteOrderLines(List<Moderateur> orderLines);
    bool DeleteOrderLine(Moderateur orderLine);
    bool OrderLineExists(CommentaireResource orderLineCreate);
    bool OrderLineExistsById(int orderLineId);
    bool Save();
}
