using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SWO.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace SWO.Server.Data
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<GradeTemplate> GradeTemplates { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Scenario> Scenarios { get; set; }

        public DbSet<Sensor> Sensors { get; set; }

        public DbSet<SensorValue> SensorValues { get; set; }

        public DbSet<Simulation> Simulations { get; set; }

        public DbSet<Simulator> Simulators { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<ScenarioGradesTemplates> ScenarioGradesTemplates { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Trainee> Trainees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Scenario>()
                .HasOne(b => b.Simulator)
                .WithMany(b => b.Scenarios)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Simulation>()
                .HasOne(b => b.Scenario)
                .WithMany(b => b.Simulations)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Simulation>()
                .HasOne(b => b.Trainee)
                .WithMany(b => b.Simulations)
                .OnDelete(DeleteBehavior.NoAction);

            SeedDatabase(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public void SeedDatabase(ModelBuilder modelBuilder)
        {
            #region roles
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Trainee",
                NormalizedName = "TRAINEE",
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Instructor",
                NormalizedName = "INSTRUCTOR",
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
            #endregion

            #region locations
            modelBuilder.Entity<Location>().HasData(new Location
            {
                ID = 1,
                Name = $"Oddział 1",
                Address = "ul. Henryka Dąbrowskiego 2\n59-100 Lublin",
                Website = $"https://drone.com/lublin",
                Description = "Ośrodek szkoleniowy numer 1 znajdujący się w Lublinie."
            });
            modelBuilder.Entity<Location>().HasData(new Location
            {
                ID = 2,
                Name = "Oddział 2",
                Address = "ul. M. Skłodowskiej-Curie 188\n59-301 Warszawa",
                Website = $"https://drone.com/warszawa",
                Description = "Ośrodek szkoleniowy numer 2 znajdujący się w Warszawie."
            });
            modelBuilder.Entity<Location>().HasData(new Location
            {
                ID = 3,
                Name = "Oddział 3",
                Address = "ul. Słowicza 52\n31-320 Kraków",
                Website = $"https://drone.com/krakow",
                Description = "Ośrodek szkoleniowy numer 3 znajdujący się w Krakowie."
            });
            #endregion

            #region simulators
            modelBuilder.Entity<Simulator>().HasData(new Simulator
            {
                ID = 1,
                Name = "Dron 1",
                LocationID = 2,
                Type = SimType.DJI_Phantom,
                Description = "Dron DJI Phantom znajdujący się w Warszawie"
            });
            modelBuilder.Entity<Simulator>().HasData(new Simulator
            {
                ID = 2,
                Name = "Dron 2",
                LocationID = 1,
                Type = SimType.DJI_Mavic,
                Description = "DJI Mavic znajdujący się w Lublinie"
            });
            modelBuilder.Entity<Simulator>().HasData(new Simulator
            {
                ID = 3,
                Name = "Dron 3",
                LocationID = 2,
                Type = SimType.DJI_Mini,
                Description = "DJI Mini znajdujący się w Warszawie"
            });
            modelBuilder.Entity<Simulator>().HasData(new Simulator
            {
                ID = 4,
                Name = "Symulator VR DJI Phantom",
                LocationID = 1,
                Type = SimType.VR_DJI_Phantom,
                Description = "Symulator VR DJI Phantom znajdujący się w Lublinie"
            });
            modelBuilder.Entity<Simulator>().HasData(new Simulator
            {
                ID = 5,
                Name = "Symulator VR DJI Mavic",
                LocationID = 2,
                Type = SimType.VR_DJI_Mavic,
                Description = "Symulator VR DJI Mavic znajdujący się w Warszawie"
            });
            modelBuilder.Entity<Simulator>().HasData(new Simulator
            {
                ID = 6,
                Name = "Symulator VR DJI Mini",
                LocationID = 1,
                Type = SimType.VR_DJI_Mini,
                Description = "Symulator VR DJI Mini znajdujący się w  Lublinie"
            });
            #endregion

            #region gradeTemplates for Sim1
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 1,
                Name = "DJI_Phantom.1.1",
                Description = "poziomy równy spąg",
                MaxPoints = 2,
                SimulatorID = 1,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 2,
                Name = "DJI_Phantom.1.2",
                Description = "organ roboczy położony na spągu",
                MaxPoints = 2,
                SimulatorID = 1,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 3,
                Name = "DJI_Phantom.1.3",
                Description = "ustawnie dźwigni w pozycji neutralnej",
                MaxPoints = 2,
                SimulatorID = 1,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 4,
                Name = "DJI_Phantom.1.4",
                Description = "jeżeli nie jest to miejsce parkowania to włączone światła",
                MaxPoints = 2,
                SimulatorID = 1,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 5,
                Name = "DJI_Phantom.1.5",
                Description = "zaciągnięcie hamulca postojowego HAP",
                MaxPoints = 3,
                SimulatorID = 1,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 6,
                Name = "DJI_Phantom.1.6",
                Description = "odpięcie pasów bezpieczeństwa",
                MaxPoints = 0,
                SimulatorID = 1,
                Note = "Sytuacja wymagana",
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 7,
                Name = "DJI_Phantom.1.7",
                Description = "zgaszenie maszyny",
                MaxPoints = 1,
                SimulatorID = 1,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 8,
                Name = "DJI_Phantom.1.8",
                Description = "założenie okularów ochronnych przed wyjściem z maszyny",
                MaxPoints = 1,
                SimulatorID = 1,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 9,
                Name = "DJI_Phantom.1.9",
                Description = "zabranie aparatu ucieczkowego",
                MaxPoints = 2,
                SimulatorID = 1,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 10,
                Name = "DJI_Phantom.1.10",
                Description = "podklinowanie maszyny",
                MaxPoints = 3,
                SimulatorID = 1,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });

            #endregion

            #region gradeTemplates for Sim2
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 11,
                Name = "DJI_Mavic.1.1",
                Description = "postój w wyznaczonym miejscu (swobodny załadunek obudowy)",
                MaxPoints = 3,
                SimulatorID = 2,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 12,
                Name = "DJI_Mavic.1.2",
                Description = "brak wyznaczonego miejsca, maszyna pozostaje na włączonych światłach",
                MaxPoints = 2,
                SimulatorID = 2,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 13,
                Name = "DJI_Mavic.1.3",
                Description = "maksymalne opuszczenie organu roboczego",
                MaxPoints = 2,
                SimulatorID = 2,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 14,
                Name = "DJI_Mavic.1.4",
                Description = "ustawnie dźwigni w pozycji neutralnej",
                MaxPoints = 2,
                SimulatorID = 2,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 15,
                Name = "DJI_Mavic.1.5",
                Description = "jeżeli nie jest to miejsce parkowania to włączone światła",
                MaxPoints = 2,
                SimulatorID = 2,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 16,
                Name = "DJI_Mavic.1.6",
                Description = "zaciągnięcie hamulca postojowego HAP",
                MaxPoints = 2,
                SimulatorID = 2,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 17,
                Name = "DJI_Mavic.1.7",
                Description = "odpięcie pasów bezpieczeństwa",
                MaxPoints = 0,
                SimulatorID = 2,
                Note = "Sytuacja wymagana",
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 18,
                Name = "DJI_Mavic.1.8",
                Description = "zgaszenie maszyny",
                MaxPoints = 1,
                SimulatorID = 2,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 19,
                Name = "DJI_Mavic.1.9",
                Description = "założenie okularów ochronnych przed wyjściem z maszyny lub wyjście w goglach",
                MaxPoints = 1,
                SimulatorID = 2,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 20,
                Name = "DJI_Mavic.1.10",
                Description = "zabranie aparatu ucieczkowego",
                MaxPoints = 2,
                SimulatorID = 2,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 21,
                Name = "DJI_Mavic.1.11",
                Description = "podklinowanie maszyny",
                MaxPoints = 3,
                SimulatorID = 2,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            #endregion

            #region gradeTemplates for Sim3
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 22,
                Name = "DJI_Mini.1.1",
                Description = "poziomy równy spąg",
                MaxPoints = 2,
                SimulatorID = 3,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 23,
                Name = "DJI_Mini.1.2",
                Description = "maksymalne opuszczenie organu roboczego ",
                MaxPoints = 2,
                SimulatorID = 3,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 24,
                Name = "DJI_Mini.1.3",
                Description = "ustawnie dźwigni w pozycji neutralnej",
                MaxPoints = 2,
                SimulatorID = 3,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 25,
                Name = "DJI_Mini.1.4",
                Description = "jeżeli nie jest to miejsce parkowania to włączone światła",
                MaxPoints = 2,
                SimulatorID = 3,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 26,
                Name = "DJI_Mini.1.5",
                Description = "zaciągnięcie hamulca postojowego HAP",
                MaxPoints = 3,
                SimulatorID = 3,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 27,
                Name = "DJI_Mini.1.6",
                Description = "odpięcie pasów bezpieczeństwa",
                MaxPoints = 0,
                SimulatorID = 3,
                Note = "Sytuacja wymagana",
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 28,
                Name = "DJI_Mini.1.7",
                Description = "zgaszenie maszyny",
                MaxPoints = 1,
                SimulatorID = 3,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 29,
                Name = "DJI_Mini.1.8",
                Description = "założenie okularów ochronnych przed wyjściem z maszyny",
                MaxPoints = 1,
                SimulatorID = 3,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 30,
                Name = "DJI_Mini.1.9",
                Description = "zabranie aparatu ucieczkowego",
                MaxPoints = 2,
                SimulatorID = 3,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            modelBuilder.Entity<GradeTemplate>().HasData(new GradeTemplate
            {
                ID = 31,
                Name = "DJI_Mini.1.10",
                Description = "podklinowanie maszyny",
                MaxPoints = 3,
                SimulatorID = 3,
                Note = null,
                Phase = null,
                OptimalTime = GenerateRandomInt(),
            });
            #endregion

            #region sensors for Sim1
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 1,
                Name = "Sensor 1",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 1
            });
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 2,
                Name = "Sensor 2",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 1
            });
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 3,
                Name = "Sensor 3",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 1
            });
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 4,
                Name = "Sensor 4",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 1
            });
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 5,
                Name = "Sensor 5",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 1
            });
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 6,
                Name = "Sensor 6",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 1
            });
            #endregion

            #region sensors for Sim2
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 7,
                Name = "Sensor 1",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 2
            });
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 8,
                Name = "Sensor 2",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 2
            });
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 9,
                Name = "Sensor 3",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 2
            });
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 10,
                Name = "Sensor 4",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 2
            });
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 11,
                Name = "Sensor 5",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 2
            });
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 12,
                Name = "Sensor 6",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 2
            });
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 13,
                Name = "Sensor 7",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 2
            });
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 14,
                Name = "Sensor 8",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 2
            });
            #endregion

            #region sensors for Sim3
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 15,
                Name = "Sensor 1",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 3
            });
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 16,
                Name = "Sensor 2",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 3
            });
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 17,
                Name = "Sensor 3",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 3
            });
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 18,
                Name = "Sensor 4",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 3
            });
            modelBuilder.Entity<Sensor>().HasData(new Sensor
            {
                ID = 19,
                Name = "Sensor 5",
                Category = "Pulpit 1",
                Addendum = null,
                SimulatorID = 3
            });
            #endregion
        }

        private static int GenerateRandomInt()
        {
            using RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            byte[] val = new byte[6];
            crypto.GetBytes(val);
            int randomvalue = BitConverter.ToInt32(val, 1);
            Random rnd = new Random(randomvalue);
            return rnd.Next(0, 22);
        }
    }
}
