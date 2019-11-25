﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reserveringssysteem;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserveringssysteem.Tests
{
    [TestClass()]
    public class BoatTypeTests
    {
        [TestInitialize]
        public void Initialize()
        {
            using (ReserveringssysteemContext context = new ReserveringssysteemContext())
            {
                context.Members.Add(new Member()
                {
                    Name = "Beau ter Ham",
                    DateOfBirth = DateTime.Now,
                    Gender = Gender.Male,
                    Organisation = "Roeivereniging",
                    Address = new Address() { City = "Zwolle", HouseNumber = 42, Street = "Roeistraat", ZIP = "1234 AB" },
                    Email = "beauterham@gmail.com",
                    Password = "#",
                });
                context.Members.Add(new Member()
                {
                    Name = "Harry Snotter",
                    DateOfBirth = DateTime.Now,
                    Gender = Gender.Male,
                    Organisation = "Roeivereniging",
                    Address = new Address() { City = "Zwolle", HouseNumber = 42, Street = "Roeistraat", ZIP = "1234 AB" },
                    Email = "harrysnotter@gmail.com",
                    Password = "#",
                });
                context.Members.Add(new Member()
                {
                    Name = "Pieter Post",
                    DateOfBirth = DateTime.Now,
                    Gender = Gender.Male,
                    Organisation = "Roeivereniging",
                    Address = new Address() { City = "Zwolle", HouseNumber = 42, Street = "Roeistraat", ZIP = "1234 AB" },
                    Email = "pieterpost@gmail.com",
                    Password = "#",
                });
                context.Members.Add(new Member()
                {
                    Name = "Prog Ramma",
                    DateOfBirth = DateTime.Now,
                    Gender = Gender.Male,
                    Organisation = "Roeivereniging",
                    Address = new Address() { City = "Zwolle", HouseNumber = 42, Street = "Roeistraat", ZIP = "1234 AB" },
                    Email = "programma@gmail.com",
                    Password = "#",
                });

                context.SaveChanges();

                List<Certificate> certificates = new List<Certificate>();
                certificates.Add(context.Certificates.Where(c => c.Name == "Certificate Skiff").First());
                context.Members.Where(u => u.Name == "Prog Ramma").First().Levels = certificates;

                context.SaveChanges();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            using (ReserveringssysteemContext context = new ReserveringssysteemContext())
            {
                context.Members.RemoveRange(context.Members.Include("Levels").Include("Address").Where(m =>
                m.Name.Equals("Beau ter Ham") ||
                m.Name.Equals("Harry Snotter") ||
                m.Name.Equals("Pieter Post") ||
                m.Name.Equals("Prog Ramma")));
                context.SaveChanges();
            }
        }

        [TestMethod()]
        public void GetAvailableBoatTypes_4_User_1_Skiff_Certificate()
        {
            RecreationalTeam recreationalTeam = new RecreationalTeam();
            recreationalTeam.Users = new List<User>();

            using (ReserveringssysteemContext context = new ReserveringssysteemContext())
                foreach (Member member in context.Members.Include("Levels"))
                    recreationalTeam.Users.Add(member);

            BoatType[] boatTypes = BoatType.GetAvailableBoatTypes(recreationalTeam);

            Assert.AreEqual(0, boatTypes.Length);
        }

        [TestMethod()]
        public void GetAvailableBoatTypes_1_User_1_Skiff_Certificate()
        {
            RecreationalTeam recreationalTeam = new RecreationalTeam();
            recreationalTeam.Users = new List<User>();

            using (ReserveringssysteemContext context = new ReserveringssysteemContext())
            {
                recreationalTeam.Users.Add(context.Members.Include("Levels").Where(m => m.Name.Equals("Prog Ramma")).First());

                BoatType[] boatTypes = BoatType.GetAvailableBoatTypes(recreationalTeam);

                Assert.AreEqual(context.BoatTypes.Where(b => b.Name.Equals("Skiff")).First().ID, boatTypes.First().ID);
            }
        }
    }
}