using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class LeagueController : Controller
    {
        private readonly LeagueContext _context;
        private readonly SportContext _sportContext;


        public LeagueController(LeagueContext context, SportContext sportContext)
        {
            _context = context;
            _sportContext = sportContext;
        }

        [HttpGet]
        public IEnumerable<League> GetAll()
        {
            return _context.Leagues.ToList();
        }

        [HttpGet("{id}", Name = "GetLeague")]
        public IActionResult GetById(long id)
        {
            var item = _context.Leagues.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] League item)
        {
            if (item == null || !item.SportId.HasValue)
            {
                return BadRequest();
            }

            var league = new League
            {
                Id = item.Id,
                Name = item.Name,
                Category = _sportContext.Sports.Where(x => x.Id == item.SportId.Value).FirstOrDefault(),
            };

            _context.Leagues.Add(league);
            _context.SaveChanges();

            return CreatedAtRoute("GetLeague", new { id = item.Id }, item);
        }

        //[HttpPut("{id}")]
        //public IActionResult Update(long id, [FromBody] League item)
        //{
        //    if (item == null || item.Id != id)
        //    {
        //        return BadRequest();
        //    }

        //    var league = _context.Leagues.FirstOrDefault(t => t.Id == id);
        //    if (league == null)
        //    {
        //        return NotFound();
        //    }

        //    league.Name = item.Name;

        //    _context.Leagues.Update(league);
        //    _context.SaveChanges();
        //    return new NoContentResult();
        //}

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Leagues.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Leagues.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
