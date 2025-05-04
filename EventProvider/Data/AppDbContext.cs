﻿using EventProvider.Models;
using Microsoft.EntityFrameworkCore;

namespace EventProvider.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Event> Events { get; set; }
            
    }
}
