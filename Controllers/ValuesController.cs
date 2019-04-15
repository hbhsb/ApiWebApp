using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWebApp.Controllers
{
    [Route("Articles")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ValuesController()
        {

        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Article>> GetAll()
        {
            return ArticleRepository.Articles;
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<Article> Get(int id)
        //{
        //    Article article = ArticleRepository.Articles.Find(c => c.Id == id);
        //    if (article == null)
        //    {
        //        return NotFound(new {Mag= $"Article {id} isn't fount" });
        //    }
        //    return article;
        //}


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Article> Get(int id, string writer)
        {
            Article article = null;
            if (string.IsNullOrEmpty(writer))
            {
                article = ArticleRepository.Articles.Find(c => c.Id == id);
            }
            else
            {
                article = ArticleRepository.Articles.Find(c => c.Id == id && c.Writer == writer);
            }
            if (article == null)
            {
                return NotFound(new { Mag = $"Article {id} isn't fount" });
            }
            return article;
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ArticleRepository.Articles.Add(article);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }

        [HttpPatch("{id}")]
        public void Patch(int id)
        {

        }


    }
}
