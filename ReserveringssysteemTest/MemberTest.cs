﻿#define test
using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reserveringssysteem;

namespace ReserveringssysteemTest
{
    [TestClass]
    public class MemberTest
    {



        [TestInitialize]
        public void MemberTestSetup()
        {
            Program.TestMode = true;

            Member member1 = new Member
            {
                Name = "Harm van der Vegt",
                DateOfBirth = DateTime.Now,
                Gender = Gender.Male,
                Organisation = "HBO-ICT",
                Email = "harmvegt@gmail.com",
                Password = "9a2bca3b828e24f91baa5dd22a49919c14d60efa1375cbdf28ac7b6ff585c2b8"
            };

            Address address = new Address
            {
                Street = "Boomkensdiep",
                HouseNumber = 32,
                ZIP = "8032XX",
                City = "Zwolle"
            };



            member1.Address = address;
            address.Member = member1;

            using (var db = new ReserveringssysteemContext())
            {
                _ = db.Users.Add(member1);
                _ = db.SaveChanges();
            }
        }


        [TestMethod]
        public void MemberLoginTest()
        {
            bool result = Member.Login("harmvegt@gmail.com", "Wachtwoord");
            Assert.IsTrue(result);
            Assert.IsNotNull(Member.CurrentMember);
            Assert.AreEqual("harmvegt@gmail.com", Member.CurrentMember.Email);
        }

        

        [TestMethod]
        public void MemberLoginEmailNullTest()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => Member.Login(null, "Wachtwoord")
            );
            Assert.IsNull(Member.CurrentMember);
        }

        [TestMethod]
        public void MemberLoginPasswordNullTest()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => Member.Login("harmvegt@gmail.com", null)    
            );
            Assert.IsNull(Member.CurrentMember);
        }

        [TestMethod]
        public void MemberLoginMemberNotFoundTest()
        {
            bool result = Member.Login("test@example.com", "blabla");
            Assert.IsNull(Member.CurrentMember);
            Assert.IsFalse(result);
        }

        [TestCleanup]
        public void MemberTestCleanUp()
        {
            using (var db = new ReserveringssysteemContext())
            {
                db.Members.RemoveRange(db.Members.Include(m => m.Address).Where(m => m.Email == "harmvegt@gmail.com"));
                db.SaveChanges();
            }
        }
    }
}
