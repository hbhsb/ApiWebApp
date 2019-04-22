using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWebApp
{
    public class Catalog
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Article> Articles { get; set; }
    }

    public class Article
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required,MinLength(2)]
        public  string Writer { get; set; }
    }

    public class Repository
    {
        public static List<Article> Articles { get; set; }
        public static List<Catalog> Catalogs { get; set; }
        static Repository()
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
            Catalogs = new List<Catalog>()
            {
                new Catalog() {Id = 1, Name = "Name1", Articles = Articles.Where(a => a.Id < 4).ToList()},
                new Catalog() {Id = 2, Name = "Name2", Articles = Articles.Where(a => a.Id >= 4).ToList()},
            };
        }
    }
}
