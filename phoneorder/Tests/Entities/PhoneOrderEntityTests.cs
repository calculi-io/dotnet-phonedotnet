using System;

using NUnit.Framework;

using PhoneOrder.Entities;

namespace PhoneOrder.Tests {
	[TestFixture]
	public class PhoneOrderEntityTests {

		[Test]
		public void FirstName() {
			var p = new PhoneOrderEntity();
            const String initialValue = "Test";

			p.FirstName = initialValue;

            Assert.AreEqual(initialValue, p.FirstName);
		}

        [Test]
        public void LastName() {
            var p = new PhoneOrderEntity();
            const String initialValue = "Test";

            p.LastName = initialValue;

            Assert.AreEqual(initialValue, p.LastName);
        }

        [Test]
        public void Street() {
            var p = new PhoneOrderEntity();
            const String initialValue = "555 Fun Street";

            p.Street = initialValue;

            Assert.AreEqual(initialValue, p.Street);
        }

        [Test]
        public void City() {
            var p = new PhoneOrderEntity();
            const String initialValue = "San Dimas";

            p.City = initialValue;

            Assert.AreEqual(initialValue, p.City);
        }

         [Test]
        public void ZipCode() {
            var p = new PhoneOrderEntity();
            const UInt32 initialValue = 19773;

            p.Zipcode = initialValue;

            Assert.AreEqual(initialValue, p.Zipcode);
        }

        [Test]
        public void Phone() {
            var p = new PhoneOrderEntity();
            const String initialValue = "909-555-5555";

            p.Phone = initialValue;

            Assert.AreEqual(initialValue, p.Phone);
        }

        [Test]
        public void Status() {
            var p = new PhoneOrderEntity();
            const PhoneOrderEntity.ApprovalStatus initialValue = PhoneOrderEntity.ApprovalStatus.Open;

            p.Status = initialValue;

            Assert.AreEqual(initialValue, p.Status);
        }

        [Test]
        public void Id() {
            var p = new PhoneOrderEntity();
            Guid initialValue = Guid.NewGuid();

            p.Id = initialValue;

            Assert.AreEqual(initialValue, p.Id);
        }

        [Test]
        public void PlacedOn() {
            var p = new PhoneOrderEntity();
            DateTime initialValue = DateTime.UtcNow;

            p.PlacedOn = initialValue;

            Assert.AreEqual(initialValue, p.PlacedOn);
        }
	}
}