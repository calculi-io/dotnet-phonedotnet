/*
 * User: Adam
 * Date: 9/9/2016
 * Time: 9:26 AM
 * 
 */
using System;
using NUnit.Framework;
using PhoneOrder.Entities;

namespace PhoneOrder.Tests
{
	[TestFixture]
	public class PhoneOrderEntityFactoryTests
	{
		[Test]
		public void SetName() {
			PhoneOrderEntity orderInstance;
			var factory = new PhoneOrderEntityFactory();
			const String
				fname = "John",
				lname = "Smith";

			factory.SetName(fname, lname);
			orderInstance = factory.GetInstance();
			
			Assert.AreEqual(fname, orderInstance.FirstName);
			Assert.AreEqual(lname, orderInstance.LastName);
		}
		
		[Test]
		public void SetStreet() {
			PhoneOrderEntity orderInstance;
			var factory = new PhoneOrderEntityFactory();
			const String street = "555 Happy Street";
			
			factory.SetStreet(street);
			orderInstance = factory.GetInstance();
			
			Assert.AreEqual(street, orderInstance.Street);
		}
		
		[Test]
		public void SetCity() {
			PhoneOrderEntity orderInstance;
			var factory = new PhoneOrderEntityFactory();
			const String city = "San Diego";
			
			factory.SetCity(city);
			orderInstance = factory.GetInstance();
			
			Assert.AreEqual(city, orderInstance.City);
		}
		
		[Test]
		public void SetZipCode() {
			PhoneOrderEntity orderInstance;
			var factory = new PhoneOrderEntityFactory();
			const UInt32 zip = 12345;
			
			factory.SetZipcode(zip);
			orderInstance = factory.GetInstance();
			
			Assert.AreEqual(zip, orderInstance.Zipcode);
		}
		
		[Test]
		public void SetPhoneNumber() {
			PhoneOrderEntity orderInstance;
			var factory = new PhoneOrderEntityFactory();
			const String phoneNumber = "555-555-5555";
			
			factory.SetPhoneNumber(phoneNumber);
			orderInstance = factory.GetInstance();
			
			Assert.AreEqual(phoneNumber, orderInstance.Phone);
		}
		
		[Test]
		public void SetOrderStatus() {
			PhoneOrderEntity orderInstance;
			var factory = new PhoneOrderEntityFactory();
			const PhoneOrderEntity.ApprovalStatus status = PhoneOrderEntity.ApprovalStatus.Open;
			
			factory.SetStatus(status);
			orderInstance = factory.GetInstance();
			
			Assert.AreEqual(status, orderInstance.Status);
		}
		
		[Test]
		public void SetPlacedOnDate() {
			PhoneOrderEntity orderInstance;
			var factory = new PhoneOrderEntityFactory();
			var time = DateTime.UtcNow;
			
			factory.SetPlacedOnDate(time);
			orderInstance = factory.GetInstance();
			
			Assert.AreEqual(time, orderInstance.PlacedOn);
		}
		
		[Test]
		public void GeneratePlacedOnDate() {
			PhoneOrderEntity orderInstance;
			var factory = new PhoneOrderEntityFactory();
			
			factory.GeneratePlacedOnDate();
			orderInstance = factory.GetInstance();
			
			Assert.NotNull(orderInstance.PlacedOn);
		}
		
		[Test]
		public void GenerateId() {
			PhoneOrderEntity orderInstance;
			var factory = new PhoneOrderEntityFactory();
			
			factory.GenerateId();
			orderInstance = factory.GetInstance();
			
			Assert.NotNull(orderInstance.Id);
		}
		
		[Test]
		public void GetInstance() {
			PhoneOrderEntity orderInstance;
			var factory = new PhoneOrderEntityFactory();
			
			orderInstance = factory.GetInstance();
			
			Assert.NotNull(orderInstance);
		}
		
		[Test]
		public void Reset() {
			PhoneOrderEntity orderInstance;
			var factory = new PhoneOrderEntityFactory();
			Guid emptyGuid = Guid.Parse("00000000-0000-0000-0000-000000000000");
			DateTime emptyTime = DateTime.FromBinary(0);
			
			factory.SetName("a", "b");
			factory.SetPhoneNumber("5555");
			factory.SetStreet("444 street");
			factory.SetCity("some city");
			factory.SetZipcode(12345);
			factory.GenerateId();
			factory.GeneratePlacedOnDate();
			factory.SetStatus(PhoneOrderEntity.ApprovalStatus.Open);
			factory.Reset();
			orderInstance = factory.GetInstance();
			
			Assert.Null(orderInstance.FirstName);
			Assert.Null(orderInstance.LastName);
			Assert.Null(orderInstance.Phone);
			Assert.Null(orderInstance.Street);
			Assert.Null(orderInstance.City);
			Assert.AreEqual(0, orderInstance.Zipcode);
			Assert.NotNull(orderInstance.Id);
			Assert.AreEqual(emptyGuid, orderInstance.Id);
			Assert.NotNull(orderInstance.PlacedOn);
			Assert.AreEqual(emptyTime, orderInstance.PlacedOn);
			Assert.AreEqual(PhoneOrderEntity.ApprovalStatus.PendingApproval, orderInstance.Status);
		}
		
		[Test]
		public void ReadFromJson() {
			PhoneOrderEntity orderInstance;
			var factory = new PhoneOrderEntityFactory();
			const String json = "{\"first_name\":\"John\",\"last_name\":\"Smith\"}";
			
			factory.FromJson(json);
			orderInstance = factory.GetInstance();
			
			Assert.AreEqual("John", orderInstance.FirstName);
			Assert.AreEqual("Smith", orderInstance.LastName);
		}
	}
}
