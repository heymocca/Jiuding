﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace airtton.Models
{
    public class News
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public string ImageUrl { get; set; }
    }

    public class Events
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public string ImageUrl { get; set; }
    }

    public class GroupIntro
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int ParentId { get; set; }
        public int Lan { get; set; }
    }

    public class PresidentDetail
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Google { get; set; }
        public string LinkedIn { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public string ImageUrl { get; set; }
        public int ParentId { get; set; }
        public int Lan { get; set; }
    }

    public class Honor
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int ParentId { get; set; }
        public int Lan { get; set; }
    }

    public class Organization
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int ParentId { get; set; }
        public int Lan { get; set; }
    }

    public class Career
    {
        public int ID { get; set; }
        public string JobTitle { get; set; }
        public string Location { get; set; }
        public string Experience { get; set; }
        public string Education { get; set; }
        public string WorkType { get; set; }
        public int VacancyNubmer { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Description { get; set; }
        public string Qualification { get; set; }
    }

    public class JobApply
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Resume { get; set; }
    }

    public class NewsDBContext : DbContext
    {
        public DbSet<News> News { get; set; }
        public DbSet<Events> Events { get; set; }

        public DbSet<GroupIntro> GroupIntro { get; set; }

        public DbSet<PresidentDetail> PresidentDetail { get; set; }

        public DbSet<Honor> Honor { get; set; }

        public DbSet<Organization> Organization { get; set; }
        
        public DbSet<Career> Career { get; set; }

        public DbSet<JobApply> JobApply { get; set; }

        //public System.Data.Entity.DbSet<airtton.ViewModel.EventsEditViewModel> EventsEditViewModels { get; set; }

        //public System.Data.Entity.DbSet<airtton.ViewModel.EventsSummaryViewModel> EventsSummaryViewModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<GroupIntro>().ToTable("GroupIntro");
        }

        public System.Data.Entity.DbSet<airtton.ViewModel.GroupIntroSummaryViewModel> GroupIntroSummaryViewModels { get; set; }

        public System.Data.Entity.DbSet<airtton.ViewModel.PresidentDetailSummaryViewModel> PresidentDetailSummaryViewModels { get; set; }

        public System.Data.Entity.DbSet<airtton.ViewModel.PresidentDetailEditViewModel> PresidentDetailEditViewModels { get; set; }
    }



}