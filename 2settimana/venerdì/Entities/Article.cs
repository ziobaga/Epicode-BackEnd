using System.ComponentModel.DataAnnotations;

namespace venerdì.Entities
{
    public class Article
    {
        public int Id { get; set; }

        
        public string Name { get; set; }

        
        
        public decimal Price { get; set; }

      
        public string Description { get; set; }

        public DateTime PublishedAt { get; set; }

        
        public IFormFile CoverImage { get; set; }

        public IFormFile AdditionalImage1 { get; set; }

        public IFormFile AdditionalImage2 { get; set; }
    }
}
