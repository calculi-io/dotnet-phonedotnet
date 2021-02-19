/*
 * User: Adam
 * Date: 9/2/2016
 * Time: 1:03 PM
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using NMemory;
using NMemory.Tables;
using PhoneOrder.Entities;
using PhoneOrder.DTO;

namespace PhoneOrder.Repositories
{
	/// <summary>
	/// Description of PhoneOrderRepository.
	/// </summary>
	public class PhoneOrderRepository : Database {
		
		static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		
		static ITable<PhoneOrderEntity> phoneOrders;
		
		public ITable<PhoneOrderEntity> PhoneOrders {
			get {
				return phoneOrders;
			}			
		}		
		
		public PhoneOrderRepository() {
			if (null == phoneOrders) {
				var phoneOrderTable = Tables.Create<PhoneOrderEntity, Guid>(u => u.Id, null);
				phoneOrders = phoneOrderTable;
			}
		}
		
		public virtual void InsertPhoneOrder(PhoneOrderEntity order) {
			PhoneOrders.Insert(order);
		}
		
		public virtual Int32 GetRecordCount() {
			IQueryable<PhoneOrderEntity> data = from p in PhoneOrders select p;
			return data.Count();
		}
		
		public virtual List<PhoneOrderEntity> GetOrderList() {
			IQueryable<PhoneOrderEntity> data = from p in PhoneOrders select p;
			return data.ToList();
		}
		
		public virtual void Reset() {
			var items = PhoneOrders.Where(x=>true);
			foreach (var i in items) {
				logger.Info("Reset-Delete: " + i.FirstName);
				PhoneOrders.Delete(i);
			}
		}
		
		public virtual bool UpdateStatus(ChangeStatusRequestDTO request) {
			IQueryable<PhoneOrderEntity> orders = PhoneOrders.Where(x=>x.Id == request.Id);
			if (orders.Count() != 1) {
				return false;
			}
			PhoneOrderEntity order = orders.First();
			order.Status = request.Status;
			PhoneOrders.Update(order);
			return true;
		}
	}
}
