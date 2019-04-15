using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWebApp
{
    public class Article
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "article"),Required]
        public string Name { get; set; }

        public  string Writer { get; set; }
    }

    public class ArticleRepository
    {
        public static List<Article> Articles { get; set; }
        static ArticleRepository()
        {
            Articles = new List<Article>()
            {
                new Article(){Id = 1,Name = "Article1",Writer = "Writer1"},
                new Article(){Id = 2,Name = "Article2",Writer = "Writer2"},
                new Article(){Id = 3,Name = "Article3",Writer = "Writer3"},
                new Article(){Id = 4,Name = "Article4",Writer = "Writer4"},
                new Article(){Id = 5,Name = "Article5",Writer = "Writer5"},
                new Article(){Id = 6,Name = "Article6",Writer = "Writer6"},
            };
        }
    }
}
