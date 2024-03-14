using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DbContext
{
	public class AppDbContext : IdentityDbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{ }
		public DbSet<ApplicationUser> ApplicationUser { get; set; }
		public DbSet<Client> Clients { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Tracking> Trackings { get; set; }
	}
}
