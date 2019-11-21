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
    public class ParticipatingTeamController : Controller
    {
        private readonly ParticipatingTeamContext _context;

        public ParticipatingTeamController(ParticipatingTeamContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ParticipatingTeam> GetAll()
        {
            return _context.ParticipatingTeams.ToList();
        }
    }
}
