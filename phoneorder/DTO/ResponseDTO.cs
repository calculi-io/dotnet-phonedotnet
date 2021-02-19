/*
 * User: Adam
 * Date: 9/7/2016
 * Time: 4:16 PM
 * 
 */
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PhoneOrder.Entities;

namespace PhoneOrder.DTO
{
	/// <summary>
	/// Description of ResponseDTO.
	/// </summary>
	public class ResponseDTO
	{
		[JsonProperty("result")]
		public String Result { get; set; }
		
		[JsonProperty("count")]
		public Int32 Count { get; set; }
		
		[JsonProperty("orders")]
		public List<PhoneOrderEntity> Orders { get; set; }
	}
}
