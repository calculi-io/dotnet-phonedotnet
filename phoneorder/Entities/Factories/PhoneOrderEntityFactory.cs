/*
 * User: Adam
 * Date: 9/7/2016
 * Time: 10:28 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using Newtonsoft.Json;

namespace PhoneOrder.Entities
{
	/// <summary>
	/// Description of PhoneOrderEntityFactory.
	/// </summary>
	public class PhoneOrderEntityFactory
	{
		PhoneOrderEntity phoneOrderEntity;
		
		public PhoneOrderEntityFactory() {
			phoneOrderEntity = new PhoneOrderEntity();
		}
		
		public void SetName(String firstName, String lastName) {
			phoneOrderEntity.FirstName = firstName;
			phoneOrderEntity.LastName = lastName;
		}
		
		public void SetStreet(String address) {
			phoneOrderEntity.Street = address;
		}
		
		public void SetCity(String city) {
			phoneOrderEntity.City = city;
		}
		
		public void SetZipcode(UInt32 zipcode) {
			phoneOrderEntity.Zipcode = zipcode;
		}
		
		public void SetPhoneNumber(String phone) {
			phoneOrderEntity.Phone = phone;
		}
		
		public void SetStatus(PhoneOrderEntity.ApprovalStatus status) {
			phoneOrderEntity.Status = status;
		}
		
		public void SetPlacedOnDate(DateTime date) {
			phoneOrderEntity.PlacedOn = date;
		}
		
		public void GeneratePlacedOnDate() {
			phoneOrderEntity.PlacedOn = DateTime.UtcNow;
		}
		
		public void GenerateId() {
			phoneOrderEntity.Id = Guid.NewGuid();
		}
		
		public virtual PhoneOrderEntity GetInstance() {
			return phoneOrderEntity;
		}
		
		public void Reset() {
			phoneOrderEntity = new PhoneOrderEntity();
		}
		
		public void FromJson(String json) {
			phoneOrderEntity = JsonConvert.DeserializeObject<PhoneOrderEntity>(json);
		}
		
	}
}
