/*
 * User: Adam
 * Date: 9/2/2016
 * Time: 10:35 AM
 * 
 */
 
using System;

using Newtonsoft.Json;

namespace PhoneOrder.Entities
{
	/// <summary>
	/// Description of PhoneOrder.
	/// </summary>
	public class PhoneOrderEntity	{

		public enum ApprovalStatus { 
			PendingApproval, 
			PendingActivation, 
			Closed, 
			Open, 
			Rejected
		};
		
		[JsonProperty("first_name")]
		public String FirstName { get; set; }
		
		[JsonProperty("last_name")]
		public String LastName { get; set; }
		
		[JsonProperty("street")]
		public String Street { get; set; }
		
		[JsonProperty("city")]
		public String City { get; set; }
		
		[JsonProperty("zipcode")]
		public UInt32 Zipcode { get; set; }
		
		[JsonProperty("phone_number")]
		public String Phone { get; set; }
		
		[JsonProperty("status")]
		public ApprovalStatus Status { get; set; }
		
		[JsonProperty("id")]
		public Guid Id { get; set; }
		
		[JsonProperty("placed_on")]
		public DateTime PlacedOn { get; set; }
    
	}
}
