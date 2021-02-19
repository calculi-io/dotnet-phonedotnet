/*
 * User: Adam
 * Date: 9/12/2016
 * Time: 9:11 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;
using NMock;
using NMemory.Tables;

using PhoneOrder.Repositories;
using PhoneOrder.Entities;
using PhoneOrder.DTO;

namespace PhoneOrder.Tests
{
	[TestFixture]
	public class PhoneOrderRepositoryTests : IDisposable {
		
		NMock.MockFactory mockFactory = new NMock.MockFactory();
		
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
		public void InsertPhoneOrder() {
			PhoneOrderRepository phoneOrderRepository = new PhoneOrderRepository();
			
			Mock<ITable<PhoneOrderEntity>> mockPhoneOrders = mockFactory.CreateMock<ITable<PhoneOrderEntity>>();
			mockPhoneOrders.Expects.One.Method(x => x.Insert(null)).WithAnyArguments();
			phoneOrderRepository.SetPrivateField("phoneOrders", mockPhoneOrders.MockObject);
			
			phoneOrderRepository.InsertPhoneOrder(new PhoneOrderEntity());
		}

		[Test]
		public void GetRecordCount() {
			PhoneOrderRepository phoneOrderRepository = new PhoneOrderRepository();
			IQueryable<PhoneOrderEntity> plist = new List<PhoneOrderEntity>{new PhoneOrderEntity{FirstName="Adam"}}.AsQueryable();
			Mock<ITable<PhoneOrderEntity>> mockPhoneOrders = mockFactory.CreateMock<ITable<PhoneOrderEntity>>();
			Int32 cnt;
			
			mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.One.GetProperty(m=>m.Provider).WillReturn(plist.Provider);
			mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.One.GetProperty(m=>m.Expression).WillReturn(plist.Expression);
			//mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.AtLeastOne.GetProperty(m=>m.ElementType).WillReturn(plist.ElementType);
			//mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.AtLeastOne.Method(m=>m.GetEnumerator()).WillReturn(plist.GetEnumerator());			
			
			phoneOrderRepository.SetPrivateField("phoneOrders", mockPhoneOrders.MockObject);
			
			cnt = phoneOrderRepository.GetRecordCount();
			
			Assert.AreEqual(1, cnt);
		}
		
		[Test]
		public void Reset() {
			PhoneOrderRepository phoneOrderRepository = new PhoneOrderRepository();
			
			IQueryable<PhoneOrderEntity> plist = new List<PhoneOrderEntity>{new PhoneOrderEntity{FirstName="Adam"}}.AsQueryable();
			
			Mock<ITable<PhoneOrderEntity>> mockPhoneOrders = mockFactory.CreateMock<ITable<PhoneOrderEntity>>();
			
			mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.One.GetProperty(m=>m.Provider).WillReturn(plist.Provider);
			mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.One.GetProperty(m=>m.Expression).WillReturn(plist.Expression);
			//mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.AtLeastOne.GetProperty(m=>m.ElementType).WillReturn(plist.ElementType);
			//mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.AtLeastOne.Method(m=>m.GetEnumerator()).WillReturn(plist.GetEnumerator());
			mockPhoneOrders.Expects.One.Method(d=>d.Delete(null)).WithAnyArguments();
			phoneOrderRepository.SetPrivateField("phoneOrders", mockPhoneOrders.MockObject);
			
			phoneOrderRepository.Reset();
		}
		
		// keep this test in case we decide to switch to Moq
		/*
		[Test]
		public void ResetWithMoq() {
			PhoneOrderRepository phoneOrderRepository = new PhoneOrderRepository();
			
			IQueryable<PhoneOrderEntity> plist = new List<PhoneOrderEntity>{new PhoneOrderEntity{FirstName = "Adam"}}.AsQueryable();
			
			Moq.Mock<ITable<PhoneOrderEntity>> mockPhoneOrders = new Moq.Mock<ITable<PhoneOrderEntity>>();
			mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Setup(m=>m.Provider).Returns(plist.Provider);
			mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Setup(m=>m.Expression).Returns(plist.Expression);
			mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Setup(m=>m.ElementType).Returns(plist.ElementType);
			mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Setup(m=>m.GetEnumerator()).Returns(plist.GetEnumerator());
			
			phoneOrderRepository.SetPrivateField("phoneOrders", mockPhoneOrders.Object);
			
			phoneOrderRepository.Reset();
		}
		*/
		
		[Test]
		public void GetOrderList() {
			PhoneOrderRepository phoneOrderRepository = new PhoneOrderRepository();
			IQueryable<PhoneOrderEntity> plist = new List<PhoneOrderEntity>{new PhoneOrderEntity{FirstName="Adam"}}.AsQueryable();
			Mock<ITable<PhoneOrderEntity>> mockPhoneOrders = mockFactory.CreateMock<ITable<PhoneOrderEntity>>();
			List<PhoneOrderEntity> orderList;
			
			mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.One.GetProperty(m=>m.Provider).WillReturn(plist.Provider);
			mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.One.GetProperty(m=>m.Expression).WillReturn(plist.Expression);
			//mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.AtLeastOne.GetProperty(m=>m.ElementType).WillReturn(plist.ElementType);
			//mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.AtLeastOne.Method(m=>m.GetEnumerator()).WillReturn(plist.GetEnumerator());			
			
			phoneOrderRepository.SetPrivateField("phoneOrders", mockPhoneOrders.MockObject);
			
			orderList = phoneOrderRepository.GetOrderList();
			
			Assert.AreEqual(1, orderList.Count);
		}
		
		[Test]
		public void UpdateStatus() {
			PhoneOrderRepository phoneOrderRepository = new PhoneOrderRepository();
			ChangeStatusRequestDTO request = new ChangeStatusRequestDTO();
			request.Id = Guid.NewGuid();
			IQueryable<PhoneOrderEntity> plist = new List<PhoneOrderEntity>{new PhoneOrderEntity{FirstName="Adam", Id=request.Id}}.AsQueryable();
			Mock<ITable<PhoneOrderEntity>> mockPhoneOrders = mockFactory.CreateMock<ITable<PhoneOrderEntity>>();
			bool rval;
			
			request.Status = PhoneOrderEntity.ApprovalStatus.Rejected;
			
			mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.One.GetProperty(m=>m.Provider).WillReturn(plist.Provider);
			mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.One.GetProperty(m=>m.Expression).WillReturn(plist.Expression);
			//mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.AtLeastOne.GetProperty(m=>m.ElementType).WillReturn(plist.ElementType);
			//mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.AtLeastOne.Method(m=>m.GetEnumerator()).WillReturn(plist.GetEnumerator());
			mockPhoneOrders.Expects.One.Method(m=>m.Update(null)).WithAnyArguments();
			
			phoneOrderRepository.SetPrivateField("phoneOrders", mockPhoneOrders.MockObject);
			
			rval = phoneOrderRepository.UpdateStatus(request);
			Assert.IsTrue(rval);
		}
		
		[Test]
		public void UpdateStatusFailureNotExist() {
			PhoneOrderRepository phoneOrderRepository = new PhoneOrderRepository();
			ChangeStatusRequestDTO request = new ChangeStatusRequestDTO();
			request.Id = Guid.NewGuid();
			IQueryable<PhoneOrderEntity> plist = new List<PhoneOrderEntity>{new PhoneOrderEntity{FirstName="Adam", Id=Guid.NewGuid()}}.AsQueryable();
			Mock<ITable<PhoneOrderEntity>> mockPhoneOrders = mockFactory.CreateMock<ITable<PhoneOrderEntity>>();
			bool rval;
			
			request.Status = PhoneOrderEntity.ApprovalStatus.Rejected;
			
			mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.One.GetProperty(m=>m.Provider).WillReturn(plist.Provider);
			mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.One.GetProperty(m=>m.Expression).WillReturn(plist.Expression);
			//mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.AtLeastOne.GetProperty(m=>m.ElementType).WillReturn(plist.ElementType);
			//mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Expects.AtLeastOne.Method(m=>m.GetEnumerator()).WillReturn(plist.GetEnumerator());
			mockPhoneOrders.Expects.No.Method(m=>m.Update(null)).WithAnyArguments();
			
			phoneOrderRepository.SetPrivateField("phoneOrders", mockPhoneOrders.MockObject);
			
			rval = phoneOrderRepository.UpdateStatus(request);
			Assert.IsFalse(rval);
		}
		
		// Keep in case of changing to Moq
		/*
		[Test]
		public void UpdateStatusWithMoq() {
			PhoneOrderRepository phoneOrderRepository = new PhoneOrderRepository();
			ChangeStatusRequestDTO request = new ChangeStatusRequestDTO();
			const string id = "00000000-0000-0000-0000-000000000000";
			IQueryable<PhoneOrderEntity> plist = new List<PhoneOrderEntity>{new PhoneOrderEntity{FirstName="Adam", Id = Guid.Parse(id)}}.AsQueryable();
			Moq.Mock<ITable<PhoneOrderEntity>> mockPhoneOrders = new Moq.Mock<ITable<PhoneOrderEntity>>();
			bool rval;
			
			request.Status = PhoneOrderEntity.ApprovalStatus.Rejected;
			request.Id = Guid.Parse(id);
			
			mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Setup(m=>m.Provider).Returns(plist.Provider);
			mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Setup(m=>m.Expression).Returns(plist.Expression);
			mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Setup(m=>m.ElementType).Returns(plist.ElementType);
			mockPhoneOrders.As<IQueryable<PhoneOrderEntity>>().Setup(m=>m.GetEnumerator()).Returns(plist.GetEnumerator());
			
			phoneOrderRepository.SetPrivateField("phoneOrders", mockPhoneOrders.Object);
			
			rval = phoneOrderRepository.UpdateStatus(request);
			
			Assert.IsTrue(rval);
		}
		*/
	}
}
