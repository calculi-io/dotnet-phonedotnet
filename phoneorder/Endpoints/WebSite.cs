/*
 * User: Adam
 * Date: 9/2/2016
 * Time: 9:24 AM
 * 
 */
using System;
using Nancy;

namespace PhoneOrder.Endpoints
{
	/// <summary>
	/// Show website
	/// </summary>
	public class WebSite : NancyModule
	{
		public WebSite() {
			Get["/"] = p => View["WebSite/index.html"];
		}
	}
}
