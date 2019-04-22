using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Newtonsoft.Json.Serialization;

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
        [Authorize(AuthenticationSchemes = "SimpleScheme")]
        [HttpGet]
        //[Route("Articles")]
        public ActionResult<IEnumerable<Article>> GetAll(int cataId)
        {
            if (cataId > 0)
            {
                Catalog catalog = Repository.Catalogs.First(c => c.Id == cataId);
                return catalog.Articles.ToList();
            }
            return Repository.Articles;
        }
        
        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<Article> Get(int id)
        //{
        //    Article article = Repository.Articles.Find(c => c.Id == id);
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
                article = Repository.Articles.Find(c => c.Id == id);
            }
            else
            {
                article = Repository.Articles.Find(c => c.Id == id && c.Writer == writer);
            }
            if (article == null)
            {
                return NotFound(new { Mag = $"Article {id} isn't fount" });
            }
            return article;
        }
        

        // POST api/values
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public ActionResult Post([FromBody] Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Repository.Articles.Add(article);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!Repository.Articles.Exists(a => a.Id == id))
            {
                return NotFound();
            }

            Article article = Repository.Articles.FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            if (Repository.Articles.Remove(article))
            {
                return NoContent();
            }

            return NotFound();

        }

        [HttpPatch("{id}")]
        public void Patch(int id)
        {

        }


    }
}
