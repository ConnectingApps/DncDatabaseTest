using System;
using ConnectingApps.Project.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConnectingApps.Project.Database
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
            Console.WriteLine("Context created");
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
