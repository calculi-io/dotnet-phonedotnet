using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Health {
	class Program {

		private static HttpClientHandler handler = new HttpClientHandler();
		private static HttpClient httpClient = new HttpClient(handler);
		private static Boolean done = false;
		private static Int32 timeout = 30;
		private static String URL = "";
		private static HttpStatusCode statusCode = HttpStatusCode.OK;
		private static Task<HttpResponseMessage> task;
		private static HttpResponseMessage result;

		static void Main(string[] args) {
			for (int i = 0; i < args.Length; ++i) {
				switch (args[i]) {
					case "-Uri":
						if (i + 1 < args.Length) {
							URL = args[i + 1];
						}
						break;
					case "-Seconds":
						if (i + 1 < args.Length) {
							try {
								timeout = int.Parse(args[i + 1]);
							} catch { }
						}
						break;
					default:
						break;
				}
			}

			if (timeout <= 0) {
				return;
			} else {
				timeout *= 1000;
			}

			if (URL == "") {
				return;
			}

			while (!done) {
				Thread.Sleep(timeout);
				try {
					task = httpClient.GetAsync(URL);
					task.Wait();
					result = task.Result;
				} catch {
					return;
				}
				if (result.StatusCode != statusCode) {
					done = true;
				}
				result.Dispose();
				task.Dispose();
			}
		}
	}
}
