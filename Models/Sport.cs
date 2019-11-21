using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
	public class Sport
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class SportContext : DbContext
	{
		public SportContext(DbContextOptions<SportContext> options) : base(options)
		{

		}

		public DbSet<Sport> Sports { get; set; }

	}
}
