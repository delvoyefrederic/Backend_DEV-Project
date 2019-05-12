using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.BackendDTO;

namespace API.Controllers
{
    // deze route is api/festivals omdat festivalsController het woordje festivals erin zit
    [Route("api/[controller]")]
    [ApiController]
    public class festivalsController : ControllerBase
    {
        readonly IFestivalRepo _festivalRepo;
        public festivalsController(IFestivalRepo festivalRepo)
        {
            _festivalRepo = festivalRepo;
        }

        // GET: api/festivals
        [HttpGet]
        public ActionResult<Task<List<TblFestivalsDTO>>> GetFestivals()
        {
            try
            {
                var result = _festivalRepo.GetFestivals();
                if (result != null)
                {

                    return result;
                }
                else
                {
                    return BadRequest();
                }
            }catch(Exception ex)
            {
                return new StatusCodeResult(404);
            }


        }

        // GET: api/festivals/5
        [HttpGet("{id}", Name = "GetFestival")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/festivals
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/festivals/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
