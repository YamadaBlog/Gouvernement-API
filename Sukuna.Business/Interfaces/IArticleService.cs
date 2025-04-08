using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface IArticleService
{
    bool CreateArticle(Utilisateur article);
    Utilisateur GetArticleById(int articleId);
    ICollection<Utilisateur> GetArticles();
    ICollection<Moderateur> GetOrderLinesByArticle(int articleId);
    ICollection<Utilisateur> GetArticlesOfASupplier(int supplierId);
    bool UpdateArticle(Utilisateur article);
    bool DeleteArticle(Utilisateur article);
    Utilisateur ArticleExists(UtilisateurResource articleCreate);
    bool ArticleExistsById(int articleId);

    bool Save();
}
