using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using System.Text;

namespace SMOOWebController.Backend
{
	public class CommandRequest
	{
		public string user;
		public string api;
		public string command;
	}

	public class HTTPListener
	{
		public static void Start()
		{
			HttpListener listener = new HttpListener();
			listener.Prefixes.Add("http://127.0.0.1:5000/");
			listener.Start();
			Console.WriteLine("Listening for requests at http://localhost:5000/");
			while (true)
			{
				var context = listener.GetContext();
				var request = context.Request;
				var response = context.Response;
				if (request.HttpMethod == "POST" && request.Url.AbsolutePath == "/sendcommand")
				{
					string requestBody = "";
					using (var reader = new System.IO.StreamReader(request.InputStream, request.ContentEncoding))
					{
						requestBody = reader.ReadToEnd();
					}
					CommandRequest payload = JsonConvert.DeserializeObject<CommandRequest>(requestBody);
					if (string.IsNullOrEmpty(payload?.user)|| string.IsNullOrEmpty(payload?.api))
					{
						RespondWithText(response, "Invalid user or API key.", HttpStatusCode.Unauthorized);
						continue;
					}
					JsonAPI jsonApi = new();

					var result = jsonApi.SendCommand(payload.command);
					RespondWithText(response, result, HttpStatusCode.OK);
				}
				else
				{
					RespondWithText(response, "Invalid endpoint or method.", HttpStatusCode.NotFound);
				}
			}
		}
		static void RespondWithText(HttpListenerResponse response, string text, HttpStatusCode statusCode)
		{
			response.StatusCode = (int)statusCode;
			byte[] buffer = Encoding.UTF8.GetBytes(text);
			response.ContentLength64 = buffer.Length;
			using (var output = response.OutputStream)
			{
				output.Write(buffer, 0, buffer.Length);
			}
		}
	}
}
