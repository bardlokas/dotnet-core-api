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
        private readonly TeamContext _teamContext;
        private readonly MatchContext _matchContext;


        public LeagueController(LeagueContext context, SportContext sportContext, TeamContext teamContext, MatchContext matchContext)
        {
            _context = context;
            _sportContext = sportContext;
            _teamContext = teamContext;
            _matchContext = matchContext;
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
                Category = _sportContext.Sports.FirstOrDefault(x => x.Id == item.SportId.Value),
                
            };

            _context.Leagues.Add(league);
            _context.SaveChanges();

            return CreatedAtRoute("GetLeague", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] League item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var league = _context.Leagues.FirstOrDefault(t => t.Id == id);
            if (league == null)
            {
                return NotFound();
            }

            if(!string.IsNullOrWhiteSpace(item.Name))
                league.Name = item.Name;

            if (item.Status != null)
                league.Status = item.Status;
    
            foreach(var match in item.Matches)
            {
                if(match.Id > 0)
                {
                    var matchToUpdate = _matchContext.Matches.FirstOrDefault(x => x.Id == match.Id);
                    matchToUpdate.Date = match.Date;

                    foreach(var participatingTeamUpdate in match.Teams)
                    {
                        var participatingTeam = matchToUpdate.Teams.FirstOrDefault(x => x.Id == participatingTeamUpdate.Id);
                        participatingTeam.Handicap = participatingTeamUpdate.Handicap;
                        participatingTeam.Score = participatingTeamUpdate.Score;
                        participatingTeam.Team = _teamContext.Teams.FirstOrDefault(x => x.Id == participatingTeamUpdate.TeamId);
                    }
                } else
                {
                    //Add new match to League
                    var newMatch = new Match
                    {
                        Date = match.Date,
                    };

                    foreach(var participatingTeam in match.Teams)
                    {
                        var team = _teamContext.Teams.FirstOrDefault(x => x.Id == participatingTeam.TeamId.Value);
                        var newParticipatingTeam = new ParticipatingTeam
                        {
                            Team = team,
                            Handicap = participatingTeam.Handicap,
                            Score = participatingTeam.Score,
                        };

                        newMatch.Teams.Add(newParticipatingTeam);
                    }

                    league.Matches.Add(newMatch);
                }

            }

            _context.Leagues.Update(league);
            _context.SaveChanges();
            return new NoContentResult();
        }

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
