/*
 * User: Adam
 * Date: 9/9/2016
 * Time: 11:10 AM
 * 
 */
using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using NMock;

using PhoneOrder.Entities;
using PhoneOrder.Services;
using PhoneOrder.Repositories;

namespace PhoneOrder.Tests
{
	[TestFixture]
	public class PhoneOrderServiceTests : IDisposable
	{
		MockFactory mockFactory = new MockFactory();
		
		protected virtual void Dispose(bool disposing) {
			if (disposing) {
				if (null != mockFactory) {
					mockFactory.Dispose();
					mockFactory = null;
				}
			}
		}
		
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		
		[Test]
		public void GetRecordCount() {
			PhoneOrderService phoneOrderService = new PhoneOrderService();
			Mock<PhoneOrderRepository> mockPhoneOrderRepository = mockFactory.CreateMock<PhoneOrderRepository>();
			mockPhoneOrderRepository.Expects.One.Method(x => x.GetRecordCount()).WillReturn(5);
			phoneOrderService.SetPrivateField("phoneOrderRepository", mockPhoneOrderRepository.MockObject);
			
			String response = phoneOrderService.GetRecordCount();
			
			Assert.AreEqual("{\"result\":\"Done\",\"count\":5,\"orders\":null}", response);
		}
		
		[Test]
		public void CreateDefaultOrder() {
			PhoneOrderService phoneOrderService = new PhoneOrderService();
			Mock<PhoneOrderRepository> mockPhoneOrderRepository = mockFactory.CreateMock<PhoneOrderRepository>();
			mockPhoneOrderRepository.Expects.One.Method(x => x.InsertPhoneOrder(null)).WithAnyArguments();
			phoneOrderService.SetPrivateField("phoneOrderRepository", mockPhoneOrderRepository.MockObject);
			
			String response = phoneOrderService.CreateDefaultOrder();
			Assert.AreEqual("{\"result\":\"Done\",\"count\":0,\"orders\":null}", response);
		}
		
		[Test]
		public void GetAllRecords() {
			PhoneOrderService phoneOrderService = new PhoneOrderService();
			List<PhoneOrderEntity> orders = new List<PhoneOrderEntity>();
			PhoneOrderEntityFactory phoneOrderEntityFactory = new PhoneOrderEntityFactory();
			Mock<PhoneOrderRepository> mockPhoneOrderRepository = mockFactory.CreateMock<PhoneOrderRepository>();
			const String expectedResult = "{\"result\":\"Done\",\"count\":0,\"orders\":[{\"first_name\":\"John\",\"last_name\":\"Smith\"," +
				"\"street\":null,\"city\":null,\"zipcode\":0,\"phone_number\":null,\"status\":0,\"id\":\"00000000-0000-0000-0000-000000000000\"," +
				"\"placed_on\":\"0001-01-01T00:00:00\"}]}";
			
			phoneOrderEntityFactory.SetName("John", "Smith");
			orders.Add(phoneOrderEntityFactory.GetInstance());
			mockPhoneOrderRepository.Expects.One.Method(x => x.GetOrderList()).WillReturn(orders);
			phoneOrderService.SetPrivateField("phoneOrderRepository", mockPhoneOrderRepository.MockObject);

			String response = phoneOrderService.GetAllRecords();
			
			Assert.AreEqual(expectedResult, response);
		}
		
		[Test]
		public void ResetRecords() {
			PhoneOrderService phoneOrderService = new PhoneOrderService();
			Mock<PhoneOrderRepository> mockPhoneOrderRepository = mockFactory.CreateMock<PhoneOrderRepository>();
			mockPhoneOrderRepository.Expects.One.Method(x => x.Reset());
			phoneOrderService.SetPrivateField("phoneOrderRepository", mockPhoneOrderRepository.MockObject);
			
			String response = phoneOrderService.ResetRecords();
			Assert.AreEqual("{\"result\":\"Done\",\"count\":0,\"orders\":null}", response);
		}
		
		[Test]
		public void CreatePhoneOrder() {
			PhoneOrderService phoneOrderService = new PhoneOrderService();
			PhoneOrderEntity phoneOrderEntityFromFactory = new PhoneOrderEntity();
			MemoryStream ms = new MemoryStream();
			StreamWriter sw = new StreamWriter(ms);
			String response;
			
			Mock<PhoneOrderRepository> mockPhoneOrderRepository = mockFactory.CreateMock<PhoneOrderRepository>();
			mockPhoneOrderRepository.Expects.One.Method(x => x.InsertPhoneOrder(null)).WithAnyArguments();
			phoneOrderService.SetPrivateField("phoneOrderRepository", mockPhoneOrderRepository.MockObject);
			Mock<PhoneOrderEntityFactory> mockPhoneOrderEntityFactory = mockFactory.CreateMock<PhoneOrderEntityFactory>();
			mockPhoneOrderEntityFactory.Expects.One.Method(y=>y.GetInstance()).WillReturn(phoneOrderEntityFromFactory);
			phoneOrderService.SetPrivateField("phoneOrderEntityFactory", mockPhoneOrderEntityFactory.MockObject);
			
			sw.Write("{}");
			sw.Flush();
			ms.Position = 0;
			response = phoneOrderService.CreatePhoneOrder(ms);
			
			sw.Close();
			ms.Close();
			
			Assert.AreEqual("{\"result\":\"Done\",\"count\":0,\"orders\":null}", response);
		}
		
		[Test]
		public void CreatePhoneOrderEmptyInput() {
			PhoneOrderService phoneOrderService = new PhoneOrderService();
			PhoneOrderEntity phoneOrderEntityFromFactory = new PhoneOrderEntity();
			MemoryStream ms = new MemoryStream();
			StreamWriter sw = new StreamWriter(ms);
			String response;
			
			Mock<PhoneOrderRepository> mockPhoneOrderRepository = mockFactory.CreateMock<PhoneOrderRepository>();
			mockPhoneOrderRepository.Expects.One.Method(x => x.InsertPhoneOrder(null)).WithAnyArguments();
			phoneOrderService.SetPrivateField("phoneOrderRepository", mockPhoneOrderRepository.MockObject);
			Mock<PhoneOrderEntityFactory> mockPhoneOrderEntityFactory = mockFactory.CreateMock<PhoneOrderEntityFactory>();
			mockPhoneOrderEntityFactory.Expects.One.Method(y=>y.GetInstance()).WillReturn(phoneOrderEntityFromFactory);
			phoneOrderService.SetPrivateField("phoneOrderEntityFactory", mockPhoneOrderEntityFactory.MockObject);
			
			sw.Write(".");
			sw.Flush();
			ms.Position = 0;
			response = phoneOrderService.CreatePhoneOrder(ms);
			
			sw.Close();
			ms.Close();
			
			Assert.AreEqual("{\"result\":\"Done\",\"count\":0,\"orders\":null}", response);
		}
		
		[Test]
		public void UpdateOrderStatus() {
			PhoneOrderService phoneOrderService = new PhoneOrderService();
			PhoneOrderEntity phoneOrderEntityFromFactory = new PhoneOrderEntity();
			MemoryStream ms = new MemoryStream();
			StreamWriter sw = new StreamWriter(ms);
			String response;
			
			Mock<PhoneOrderRepository> mockPhoneOrderRepository = mockFactory.CreateMock<PhoneOrderRepository>();
			mockPhoneOrderRepository.Expects.One.Method(x => x.UpdateStatus(null)).WithAnyArguments().WillReturn(true);
			phoneOrderService.SetPrivateField("phoneOrderRepository", mockPhoneOrderRepository.MockObject);
			
			sw.Write("{\"status\":1, \"id\":\"00000000-0000-0000-0000-000000000000\"}");
			sw.Flush();
			ms.Position = 0;
			
			response = phoneOrderService.UpdateOrderStatus(ms);
			
			sw.Close();
			ms.Close();
			
			Assert.AreEqual("{\"result\":\"Done\",\"count\":0,\"orders\":null}", response);
		}
		
		[Test]
		public void UpdateOrderStatusFailed() {
			PhoneOrderService phoneOrderService = new PhoneOrderService();
			PhoneOrderEntity phoneOrderEntityFromFactory = new PhoneOrderEntity();
			MemoryStream ms = new MemoryStream();
			StreamWriter sw = new StreamWriter(ms);
			String response;
			
			Mock<PhoneOrderRepository> mockPhoneOrderRepository = mockFactory.CreateMock<PhoneOrderRepository>();
			mockPhoneOrderRepository.Expects.One.Method(x => x.UpdateStatus(null)).WithAnyArguments().WillReturn(false);
			phoneOrderService.SetPrivateField("phoneOrderRepository", mockPhoneOrderRepository.MockObject);
			
			sw.Write("{\"status\":1, \"id\":\"00000000-0000-0000-0000-000000000000\"}");
			sw.Flush();
			ms.Position = 0;
			
			response = phoneOrderService.UpdateOrderStatus(ms);
			
			sw.Close();
			ms.Close();
			
			Assert.AreEqual("{\"result\":\"Incomplete\",\"count\":0,\"orders\":null}", response);
		}
	}
}
