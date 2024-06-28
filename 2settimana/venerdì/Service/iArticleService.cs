using venerdì.Entities;
using venerdì.Service;

namespace venerdì.Service
{
    public interface iArticleService
    {

       public void Create(Article entity);



         Article GetById(int entityId);
            
      

        IEnumerable<Article> GetAll();

    }
}
