using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.DataAccess.Data;


namespace Sukuna.Service.Services;

public class ArticleService : IParticipationService
{
    private readonly DataContext _context;

    public ArticleService(DataContext context)
    {
        _context = context;
    }

    public bool CreateArticle(Utilisateur article)
    {
        _context.Add(article);

        return Save();
    }

    public ICollection<Utilisateur> GetArticles()
    {
        return _context.Articles.OrderBy(p => p.ID).ToList();
    }
    public Utilisateur GetArticleById(int articleId)
    {
        return _context.Articles.Where(c => c.ID == articleId).FirstOrDefault();
    }

    public ICollection<Moderateur> GetOrderLinesByArticle(int articleId)
    {
        return _context.OrderLines.Where(r => r.Article.ID == articleId).ToList();
    }

    public ICollection<Utilisateur> GetArticlesOfASupplier(int supplierId)
    {
        return _context.Articles.Where(o => o.Supplier.ID == supplierId).ToList();
    }

    public bool UpdateArticle(Utilisateur article)
    {
        _context.Update(article);
        return Save();
    }
    public bool DeleteArticle(Utilisateur article)
    {
        _context.Remove(article);
        return Save();
    }

    public Utilisateur ArticleExists(UtilisateurResource articleCreate)
    {
        return GetArticles().Where(c => c.Nom.Trim().ToUpper() == articleCreate.Nom.TrimEnd().ToUpper())
            .FirstOrDefault();
    }

    public bool ArticleExistsById(int articleId)
    {
        return _context.Articles.Any(r => r.ID == articleId);

    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}
