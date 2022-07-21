using System;
using System.Collections.Generic;
using ems_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ems_backend.Data
{
    #pragma warning disable
    public class EMSContext : DbContext
    {
        public EMSContext(DbContextOptions<EMSContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}
