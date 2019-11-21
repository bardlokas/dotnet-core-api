using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
	public class Team
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public int? SportId { get; set; }

        public Sport Sport { get; set; }
	}

	public class TeamContext : DbContext
	{
		public TeamContext(DbContextOptions<TeamContext> options) : base(options)
		{

		}

		public DbSet<Team> Teams { get; set; }

	}
}
