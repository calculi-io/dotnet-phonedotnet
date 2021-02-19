/*
 * User: Adam
 * Date: 9/9/2016
 * Time: 8:14 AM
 * 
 */
using System;
using System.IO;
using System.Collections.Generic;

using Newtonsoft.Json;

using PhoneOrder.Entities;
using PhoneOrder.DTO;
using PhoneOrder.Repositories;

namespace PhoneOrder.Services
{
	/// <summary>
	/// Description of PhoneOrderService.
	/// </summary>
	public class PhoneOrderService {
		
		static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		
		PhoneOrderRepository phoneOrderRepository = new PhoneOrderRepository();
		PhoneOrderEntityFactory phoneOrderEntityFactory = new PhoneOrderEntityFactory();
		
		public String CreateDefaultOrder() {
			ResponseDTO responseDTO = new ResponseDTO();
			phoneOrderEntityFactory.GenerateId();
			phoneOrderEntityFactory.SetName("John", "Smith");
			phoneOrderEntityFactory.SetStreet("356 Rosewood");
			phoneOrderEntityFactory.SetCity("Happyville");
			phoneOrderEntityFactory.SetZipcode(31415);
			phoneOrderEntityFactory.SetPhoneNumber("555-5555");
			phoneOrderEntityFactory.SetStatus(PhoneOrderEntity.ApprovalStatus.Open);
			phoneOrderEntityFactory.SetPlacedOnDate(DateTime.Now);
			PhoneOrderEntity defaultOrder = phoneOrderEntityFactory.GetInstance();

			phoneOrderRepository.InsertPhoneOrder(defaultOrder);
			
			responseDTO.Result = "Done";

			return JsonConvert.SerializeObject(responseDTO);
		}
		
		public String GetRecordCount() {
			ResponseDTO responseDTO = new ResponseDTO();

			responseDTO.Count = phoneOrderRepository.GetRecordCount();
			responseDTO.Result = "Done";

			return JsonConvert.SerializeObject(responseDTO);
		}
		
		public String GetAllRecords() {
			ResponseDTO responseDTO = new ResponseDTO();
			List<PhoneOrderEntity> list = phoneOrderRepository.GetOrderList();

			responseDTO.Result = "Done";
			responseDTO.Orders = list;

			return JsonConvert.SerializeObject(responseDTO);
		}
		
		public String ResetRecords() {
			ResponseDTO responseDTO = new ResponseDTO();
				
			phoneOrderRepository.Reset();
			responseDTO.Result = "Done";
			
			return JsonConvert.SerializeObject(responseDTO);
		}
		
		public String CreatePhoneOrder(Stream body) {
			String json = getJsonFromBody(body);
			ResponseDTO responseDTO = new ResponseDTO();
			PhoneOrderEntity order;

			phoneOrderEntityFactory.FromJson(json);
			phoneOrderEntityFactory.GenerateId();
			phoneOrderEntityFactory.GeneratePlacedOnDate();
			phoneOrderEntityFactory.SetStatus(PhoneOrderEntity.ApprovalStatus.Open);
			order = phoneOrderEntityFactory.GetInstance();
			phoneOrderRepository.InsertPhoneOrder(order);
			
			responseDTO.Result = "Done";
			return JsonConvert.SerializeObject(responseDTO);
		}
		
		public String UpdateOrderStatus(Stream body) {
			ResponseDTO responseDTO = new ResponseDTO();
			String json = getJsonFromBody(body);
			bool rval;
			var changeStatusRequestDTO = JsonConvert.DeserializeObject<ChangeStatusRequestDTO>(json);
			
			rval = phoneOrderRepository.UpdateStatus(changeStatusRequestDTO);
			
			responseDTO.Result = rval?"Done":"Incomplete";
			
			return JsonConvert.SerializeObject(responseDTO);
		}
		
		String getJsonFromBody(Stream body) {
			long length = body.Length;
			byte[] buffer = new byte[length];
			int readLength = body.Read(buffer, 0, (int)length);
			return (readLength >= 2) ? System.Text.Encoding.UTF8.GetString(buffer) : "{}";
		}
	}
}
