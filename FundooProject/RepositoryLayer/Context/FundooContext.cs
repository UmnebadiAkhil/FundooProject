using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<User> UserTable { get; set; }
        public DbSet<Notes> NotesTable { get; set; }
        public DbSet<Collaboration> CollabTable { get; set; }
        public DbSet<Label> LabelTable { get; set; }
    }
}
