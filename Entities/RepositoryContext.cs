using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class RepositoryContext:DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options):base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Owner> Owners { get; set; }
    }
}
