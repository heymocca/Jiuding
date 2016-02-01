namespace airtton.Migrations
{
    using airtton.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<airtton.Models.NewsDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "airtton.Models.NewsDBContext";
        }

        protected override void Seed(airtton.Models.NewsDBContext context)
        {
            //var GroupIntros = new List<GroupIntro>
            //{
            //    new GroupIntro { ID = 1, Title = "Group Introduction", Description = "The nine tripods-China's ancient XiaYuZhu the nine tripods, symbol of kyushu.", ImageUrl = null, Lan = 0, ParentId = 0 }
            //};


        }
    }
}
