/*
 * User: Adam
 * Date: 9/2/2016
 * Time: 8:24 AM
 * 
 */
using System;
using Owin;

namespace PhoneOrder
{
	/// <summary>
	/// Entry point to .NET application
	/// </summary>
	public class Startup
	{
		private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		
		public Startup() {
		}

		public void Configuration(IAppBuilder app) {
			logger.Info("Starting app.");
			app.UseNancy();
		}
	}
}
