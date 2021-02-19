/*
 * User: Adam
 * Date: 9/2/2016
 * Time: 1:28 PM
 * 
 */
using System;
using System.Threading.Tasks;

using Nancy;

using PhoneOrder.Services;

namespace PhoneOrder.Endpoints
{
	/// <summary>
	/// Description of PhoneOrderMethods.
	/// </summary>
	public class PhoneOrderMethods : NancyModule
	{

		static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		
		PhoneOrderService phoneOrderService = new PhoneOrderService();

		public PhoneOrderMethods() : base("/PhoneOrder") {
			
			Post["/createDefault", true] = async (_, token) => {
				await Task.Delay(50);
				logger.Info("Create default Order");
				return phoneOrderService.CreateDefaultOrder();
			};
			
			Post["/count", true] = async (_, token) => {
				await Task.Delay(50);
				logger.Info("Get Count");
				return phoneOrderService.GetRecordCount();
			};
			
			Post["/listAll", true] = async (_, token) => {
				await Task.Delay(50);
				logger.Info("List All");
				return phoneOrderService.GetAllRecords();
			};

			Post["/reset", true] = async (_, token) => {
				await Task.Delay(50);
				logger.Info("reset");
				return phoneOrderService.ResetRecords();
			};

			Post["/addOrder", true] = async(_, token) => {
				await Task.Delay(50);
				logger.Info("add order");
				return phoneOrderService.CreatePhoneOrder(this.Request.Body);
			};
			
			Post["/updateStatus", true] = async(_, token) => {
				await Task.Delay(50);
				logger.Info("Update Status");
				return phoneOrderService.UpdateOrderStatus(this.Request.Body);
			};
		}
	}
}
