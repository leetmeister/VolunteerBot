namespace VolunteerDataWebApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using VolunteerDataWebApi.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<VolunteerDataWebApi.Models.VolunteerDataWebApiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(VolunteerDataWebApi.Models.VolunteerDataWebApiContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Volunteers.AddOrUpdate(p => p.Name,
                new Volunteer
                {
                    Name = "Debra Garcia",
                    Email = "debra@example.com",
                },
                new Volunteer
                {
                    Name = "Thorsten Weinrich",
                    Email = "thorsten@example.com",
                },
                new Volunteer
                {
                    Name = "Yuhong Li",
                    Email = "yuhong@example.com",
                },
                new Volunteer
                {
                    Name = "Jon Orton",
                    Email = "jon@example.com",
                },
                new Volunteer
                {
                    Name = "Diliana Alexieva-Bosseva",
                    Email = "diliana@example.com",
                }
                );

            context.Events.AddOrUpdate(p => p.Name,
                new Event
                {
                    Name = "Open House",
                    Description = "Come help us spread the word and interact with new donors and volunteers!",
                    StartDateTime = new DateTime(2016, 8, 20, 18, 0, 0, DateTimeKind.Utc),
                    Duration = new TimeSpan(5, 0, 0),
                },
                new Event
                {
                    Name = "Food Distribution Night July",
                    Description = "Our monthly Tuesday night food distribution drive for the public pantry.",
                    StartDateTime = new DateTime(2016, 7, 13, 0, 0, 0, DateTimeKind.Utc),
                    Duration = new TimeSpan(3, 0, 0),
                },
                new Event
                {
                    Name = "Food Distribution Night August",
                    Description = "Our monthly Tuesday night food distribution drive for the public pantry.",
                    StartDateTime = new DateTime(2016, 8, 17, 0, 0, 0, DateTimeKind.Utc),
                    Duration = new TimeSpan(3, 0, 0),
                }
                );

            context.VolunteerActivities.AddOrUpdate(p => p.Id,
                new VolunteerActivity
                {
                    VolunteerId = 1,
                    EventId = 2,
                    StartDateTime = new DateTime(2016, 7, 13, 0, 0, 0, DateTimeKind.Utc),
                    Duration = new TimeSpan(1, 0, 0),
                    ReportingProperties = ReportingProperties.Reviewed
                },
                new VolunteerActivity
                {
                    VolunteerId = 1,
                    EventId = 2,
                    StartDateTime = new DateTime(2016, 7, 13, 2, 0, 0, DateTimeKind.Utc),
                    Duration = new TimeSpan(1, 0, 0),
                    ReportingProperties = ReportingProperties.Reviewed
                },
                new VolunteerActivity
                {
                    VolunteerId = 3,
                    EventId = 2,
                    StartDateTime = new DateTime(2016, 7, 13, 0, 30, 0, DateTimeKind.Utc),
                    Duration = new TimeSpan(2, 30, 0),
                    ReportingProperties = ReportingProperties.Reviewed | ReportingProperties.Amended
                },
                new VolunteerActivity
                {
                    VolunteerId = 4,
                    EventId = 2,
                    StartDateTime = new DateTime(2016, 7, 13, 0, 0, 0, DateTimeKind.Utc)
                },
                new VolunteerActivity
                {
                    VolunteerId = 5,
                    EventId = 1,
                    StartDateTime = new DateTime(2016, 7, 13, 0, 30, 0, DateTimeKind.Utc),
                    Duration = new TimeSpan(2, 30, 0),
                    ReportingProperties = ReportingProperties.Invalidated | ReportingProperties.PostdatedEnd | ReportingProperties.PostdatedStart
                }
                );

            context.VolunteerIntents.AddOrUpdate(p => p.Id,
                new VolunteerIntent
                {
                    VolunteerId = 1,
                    EventId = 3,
                    StartDateTime = new DateTime(2016, 8, 17, 0, 0, 0, DateTimeKind.Utc),
                    Duration = new TimeSpan(1, 0, 0),
                    Intent = Intent.Confirmed
                },
                new VolunteerIntent
                {
                    VolunteerId = 1,
                    EventId = 3,
                    StartDateTime = new DateTime(2016, 8, 17, 2, 0, 0, DateTimeKind.Utc),
                    Duration = new TimeSpan(1, 0, 0),
                    Intent = Intent.Confirmed
                },
                new VolunteerIntent
                {
                    VolunteerId = 1,
                    EventId = 1,
                    Intent = Intent.Uninterested
                },
                new VolunteerIntent
                {
                    VolunteerId = 2,
                    EventId = 1,
                    StartDateTime = new DateTime(2016, 8, 20, 22, 0, 0, DateTimeKind.Utc),
                    Duration = new TimeSpan(1, 0, 0),
                    Intent = Intent.Interested
                },
                new VolunteerIntent
                {
                    VolunteerId = 4,
                    EventId = 1,
                    StartDateTime = new DateTime(2016, 8, 20, 20, 0, 0, DateTimeKind.Utc),
                    Intent = Intent.Interested
                },
                new VolunteerIntent
                {
                    VolunteerId = 5,
                    EventId = 1,
                    Duration = new TimeSpan(2, 30, 0),
                    Intent = Intent.Cancelled
                }
                );
        }
    }
}
