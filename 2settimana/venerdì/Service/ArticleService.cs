using venerdì.Entities;

namespace venerdì.Service
{
    public class ArticleService:iArticleService
    {
        protected static readonly List<Article> entities = new List<Article>();
        private static int lastId = 0;

        

        public void Create(Article entity)
        {
            entity.Id = ++lastId;
            entity.PublishedAt = DateTime.Now;
            entities.Add(entity);
        }

       

        public Article GetById(int id) => 
            entities.Single(e => e.Id == id);
        
        
        public IEnumerable<Article> GetAll() => entities;


    }
}
