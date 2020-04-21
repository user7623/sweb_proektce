using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SWEB_app.Models;

namespace sweb.Models
{
    public class swebContext : DbContext
    {
        public swebContext (DbContextOptions<swebContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }

        public DbSet<Teacher> Teacher { get; set; }

        public DbSet<Course> Course { get; set; }

        public DbSet<Enrollment> Enrollment { get; set; }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<Student>()
        //        .HasMany<Enrollment>(p => p.Courses);
        //    //.HasPrincipalKey(p => p.Id);
        //    builder.Entity<Enrollment>()
        //        .HasOne<Course>(p => p.mCourse)
        //        .HasForeignKey(p => p.ID);
        //}

    }
}
