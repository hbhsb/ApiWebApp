using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApiWebApp.Controllers
{
    [ApiController]
    [Route("Catalogs")]
    
    public class CatalogController : ControllerBase
    {
        public ActionResult<IEnumerable<Catalog>> Index()
        {
            return Repository.Catalogs;
        }
    }
}