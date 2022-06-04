using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DualTracking.Database
{
	public class ApplicationDbContext : IdentityDbContext<Parent>
	{		
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				//optionsBuilder.UseSqlServer(@"Server=.;Database=DualTracking;Trusted_Connection=True;");
				optionsBuilder.UseSqlServer(@"Data Source=38.17.52.91;User ID=sa;Password=Renelocojr@.19982;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Initial Catalog=DualTracking");
			}
		}

		public virtual DbSet<Parent> Parents { get; set; }
		public virtual DbSet<Child> Children { get; set; }
		public virtual DbSet<Questionnaire> Questionnaires { get; set; }
		public virtual DbSet<Response> Responses { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
{
			builder.Entity<Parent>(entity =>
			{
				entity.Ignore(e => e.Email);
				entity.Ignore(e => e.NormalizedEmail);
				entity.Ignore(e => e.AccessFailedCount);
				entity.Ignore(e => e.LockoutEnabled);
				entity.Ignore(e => e.LockoutEnd);
			});

			base.OnModelCreating(builder);
		}
	}
}
