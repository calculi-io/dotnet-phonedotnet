/*
 * User: Adam
 * Date: 9/7/2016
 * Time: 4:15 PM
 * 
 */
using System;
using Newtonsoft.Json;
using PhoneOrder.Entities;

namespace PhoneOrder.DTO
{
	/// <summary>
	/// Description of ChangeStatusRequestDTO.
	/// </summary>
	public class ChangeStatusRequestDTO
	{
		[JsonProperty("id")]
		public Guid Id { get; set; }
		
		[JsonProperty("status")]
		public PhoneOrderEntity.ApprovalStatus Status { get; set; }
	}
}
