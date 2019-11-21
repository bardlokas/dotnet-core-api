using Microsoft.EntityFrameworkCore;
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
		public List<Match> Matches { get; set; }
        public int? SportId { get; set; }
		public Sport Category { get; set; }



	}

	public class LeagueContext : DbContext
	{
		public LeagueContext(DbContextOptions<LeagueContext> options) : base(options)
		{

		}

		public DbSet<League> Leagues { get; set; }

	}
}
