using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
	public class Match
	{
		public int Id { get; set; }
		public List<ParticipatingTeam> Teams { get; set; } = new List<ParticipatingTeam>();
		public DateTime Date { get; set; } 
	}

	public class ParticipatingTeam
	{
        public int Id { get; set; }
        public int? TeamId { get; set; }
        [JsonIgnore]
		public Team Team { get; set; }
		public double Score { get; set; }
		public int Handicap { get; set; }
	}

	public class ParticipatingTeamContext : DbContext
	{
		public ParticipatingTeamContext(DbContextOptions<ParticipatingTeamContext> options) : base(options)
		{

		}

		public DbSet<ParticipatingTeam> ParticipatingTeams { get; set; }

	}

	public class MatchContext : DbContext
	{
		public MatchContext(DbContextOptions<MatchContext> options) : base(options)
		{

		}

		public DbSet<Match> Matches { get; set; }

	}
}
