using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
	public class League
	{
		public int Id { get; set; }
		public string Name { get; set; }
        [JsonIgnore]
		public List<Match> Matches { get; set; }
        public int? SportId { get; set; }
		public Sport Category { get; set; }
        public List<Team> Teams { get; set; }
        public LeagueStatus? Status { get; set; }

	}

    public enum LeagueStatus
    {
        Planned = 0,
        Ongoing = 1,
        Finished = 2,
        Cancelled = 3,
    }

	public class LeagueContext : DbContext
	{
		public LeagueContext(DbContextOptions<LeagueContext> options) : base(options)
		{

		}

		public DbSet<League> Leagues { get; set; }

	}
}
