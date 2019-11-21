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
    public class TeamController : Controller
    {
        private readonly TeamContext _context;

        public TeamController(TeamContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Team> GetAll()
        {
            return _context.Teams.ToList();
        }

        [HttpGet("{id}", Name = "GetTeam")]
        public IActionResult GetById(long id)
        {
            var item = _context.Teams.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Team item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Teams.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTeam", new { id = item.Id }, item);
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
            var todo = _context.Teams.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
