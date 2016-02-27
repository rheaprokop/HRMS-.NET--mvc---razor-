using System;  
using System.Web;
using System.Collections.Generic;
using System.Data.Entity;
using HRMS.Models; 

namespace HRMS.Models
{
    public class HRMSContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Opening> Openings { get; set; }
        public DbSet<Stage> Stages { get; set; }

        public HRMSContext()
            : base("HRMS")
        { }
         

        //fluent api for candidate model

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //create table name "CandidateDB" for candidates
            modelBuilder.Entity<Candidate>().ToTable("Candidates");
            modelBuilder.Entity<Stage>().ToTable("Stages"); 
        }
    }

}