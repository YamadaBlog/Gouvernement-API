using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface IUserService
{
    bool CreateUser(Interaction user);
    Interaction GetUserById(int userId);
    ICollection<Interaction> GetUsers();
    Interaction GetAuthauthUser(string userEmail, string userMpd);
    ICollection<Ressource> GetSupplierOrdersByUser(int userId);
    bool UpdateUser(Interaction user);
    bool DeleteUser(Interaction user);
    bool UserExists(EvenementResource userCreate);
    bool UserExistsById(int userId);

    bool Save();
}
