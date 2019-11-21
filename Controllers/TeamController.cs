﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TeamController : Controller
    {
        private readonly TeamContext _context;
        private readonly SportContext _sportContext;

        public TeamController(TeamContext context, SportContext sportContext)
        {
            _context = context;
            _sportContext = sportContext;
        }

        [HttpGet]
        public IEnumerable<Team> GetAll()
        {
            return _context.Teams.Include(x => x.Sport).ToList();
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

            var test = _sportContext.Sports.Where(x => x.Id == item.SportId).FirstOrDefault();


            var team = new Team
            {
                Id = item.Id,
                Name = item.Name,
                Sport = _sportContext.Sports.Where(x => x.Id == item.SportId).FirstOrDefault()
            };

            _context.Teams.Add(team);
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
