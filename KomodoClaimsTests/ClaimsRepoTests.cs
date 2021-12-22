using _02_KomodoClaims.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KomodoClaimsTests
{
    [TestClass]
    public class ClaimsRepoTests
    {
        [TestMethod]
        public void AddClaimTest_ShouldReturnTrue()
        {
            Claim testClaim = new Claim(10, ClaimType.Car, "Bloaty Tires", 50.99, DateTime.Parse("02/10/2020"), DateTime.Parse("03/03/2020"));
            bool success = _claimRepo.AddToQueue(testClaim);

            Assert.IsTrue(success);
        }

        ClaimRepository _claimRepo = new ClaimRepository();

        [TestInitialize]
        public void Arrange()
        {
            Claim test0 = new Claim(0, ClaimType.Car, "Hokey Pokey", 0.99, DateTime.Parse("05/10/2021"), DateTime.Parse("06/03/2021"));
            Claim test1 = new Claim(1, ClaimType.Home, "Pikachu", 30, DateTime.Parse("05/10/2021"), DateTime.Parse("05/11/2021"));
            Claim test2 = new Claim(2, ClaimType.Car, "10 car pileup", 10000, DateTime.Parse("05/10/2021"), DateTime.Parse("06/20/2021"));
            Claim test3 = new Claim(3, ClaimType.Car, "T-Boned", 5000, DateTime.Parse("05/10/2021"), DateTime.Parse("07/03/2021"));
            Claim test4 = new Claim(4, ClaimType.Theft, "Stolen wiper blades", 19.99, DateTime.Parse("05/10/2021"), DateTime.Parse("06/01/2021"));
            Claim test5 = new Claim(5, ClaimType.Theft, "Radio stolen from vehicle", 200, DateTime.Parse("05/10/2021"), DateTime.Parse("05/12/2021"));
            Claim test6 = new Claim(6, ClaimType.Car, "Cracked windshield", 198.99, DateTime.Parse("05/10/2021"), DateTime.Parse("08/03/2021"));
            Claim test7 = new Claim(7, ClaimType.Car, "broken tail light", 50.49, DateTime.Parse("04/10/2021"), DateTime.Parse("05/03/2021"));
            Claim test8 = new Claim(8, ClaimType.Car, "Popped tire", 25.99, DateTime.Parse("05/10/2021"), DateTime.Parse("06/10/2021"));
            Claim test9 = new Claim(9, ClaimType.Car, "Superficial damage", 50, DateTime.Parse("05/10/2021"), DateTime.Parse("06/12/2021"));

            _claimRepo.AddToQueue(test0);
            _claimRepo.AddToQueue(test1);
            _claimRepo.AddToQueue(test2);
            _claimRepo.AddToQueue(test3);
            _claimRepo.AddToQueue(test4);
            _claimRepo.AddToQueue(test5);
            _claimRepo.AddToQueue(test6);
            _claimRepo.AddToQueue(test7);
            _claimRepo.AddToQueue(test8);
            _claimRepo.AddToQueue(test9);
        }

        [TestMethod]
        public void ListClaimsTest_ShouldReturnExpectedArray()
        {
            Claim[] testList = _claimRepo.ListClaims();

            int expected = 10;
            int actual = testList.Length;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DisplayNextClaimTest_ShouldReturnExpectedClaimObject()
        {
            Claim testClaim = _claimRepo.DisplayNextClaim();
            int expected = 0;
            int actual = testClaim.ID;

            Assert.IsNotNull(testClaim);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProcessClaimTest_ShouldReturnExpectedClaimObjectAndAdvanceQueue()
        {
            Claim testClaim = _claimRepo.ProcessClaim();
            Claim nextClaim = _claimRepo.DisplayNextClaim();

            int expected = 0;
            int actual = testClaim.ID;
            int expectedNext = 1;
            int actualNext = nextClaim.ID;
            

            Assert.IsNotNull(testClaim);
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedNext, actualNext);
        }
    }
}
