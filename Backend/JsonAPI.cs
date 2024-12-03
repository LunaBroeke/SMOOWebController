using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace SMOOWebController.Backend
{
	public class Costume
	{
		public string Cap { get; set; }
		public string Body { get; set; }
	}

	public class PersistShines
	{
		public bool Enabled { get; set; }
	}

	public class Player
	{
		public string ID { get; set; }
		public string Name { get; set; }
		public int? GameMode { get; set; }
		public string Kingdom { get; set; }
		public string Stage { get; set; }
		public int Scenario { get; set; }
		public Position Position { get; set; }
		public Rotation Rotation { get; set; }
		public bool? Tagged { get; set; }
		public Costume Costume { get; set; }
		public string? Capture { get; set; }
		public bool? Is2D { get; set; }
		public string IPv4 { get; set; }
	}

	public class Position
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }
	}

	public class Root
	{
		public SMOOSettings Settings { get; set; }
		public List<Player> Players { get; set; }
	}

	public class Rotation
	{
		public double W { get; set; }
		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }
	}

	public class Scenario
	{
		public bool MergeEnabled { get; set; }
	}

	public class Server
	{
		public int MaxPlayers { get; set; }
	}

	public class SMOOSettings
	{
		public PersistShines PersistShines { get; set; }
		public Scenario Scenario { get; set; }
		public Server Server { get; set; }
		public Shines Shines { get; set; }
	}

	public class Shines
	{
		public bool Enabled { get; set; }
	}


	public class JsonAPI
	{
		public Settings settings = Settings.LoadSettings();
		private Logger logger = new Logger() { name = "JsonAPI"};
		/// <summary>
		/// General Request Sender to the server using the JsonAPI objects settings.token.
		/// </summary>
		/// <param name="request">The API_JSON_REQUEST object detailing if its a Status or a Command request and its command</param>
		/// <returns>Full Json string returned by the request</returns>
		private string SendRequestToServer(APIRequest request)
		{
			RequestRoot r = new RequestRoot() { API_JSON_REQUEST = request };
			string json = JsonConvert.SerializeObject(r);
			string response = string.Empty;
			int attempt = 0;
			do
			{
				try
				{
					using (var client = new TcpClient())
					{
						client.ReceiveTimeout = 500;
						client.SendTimeout = 500;
						var result = client.BeginConnect(settings.smooServer.Address, settings.smooServer.Port, null, null);
						bool success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(2));
						if (!success)
						{
							throw new SocketException();
						}
						using (var stream = client.GetStream())
						using (var writer = new StreamWriter(stream))
						using (var reader = new StreamReader(stream))
						{
							writer.Write(json);
							writer.Flush();
							response = reader.ReadToEnd();
							return response;
						}
					}
				}
				catch
				{
					logger.Log($"failed calling {settings.smooServer.Address}:{settings.smooServer.Address}");
					attempt++;
				}
			}
			while (attempt < 10);
			return "Failed";
		}
		/// <summary>
		/// Requests for the full root of the "Status" type.
		/// </summary>
		/// <returns>Full Status Root</returns>
		public Root RequestData()
		{
			APIRequest request = new APIRequest()
			{
				Token = settings.token,
			};
			return JsonConvert.DeserializeObject<Root>(SendRequestToServer(request));
		}
		/// <summary>
		/// Requests to send a command to the server.
		/// </summary>
		/// <param name="command">SMOO server command</param>
		/// <returns>full string that gets returned by the server</returns>
		public string SendCommand(string command)
		{
			APIRequest request = new APIRequest()
			{
				Token = settings.token,
				Type = "Command",
				Data = command
			};
			return SendRequestToServer(request);
		}
		/// <summary>
		/// Gets the API tokens current permissions
		/// </summary>
		/// <returns>List of permissions</returns>
		public string GetPermissions()
		{
			APIRequest request = new APIRequest()
			{
				Token = settings.token,
				Type = "Permissions"
			};
			string s = SendRequestToServer(request);
			var result = JsonConvert.DeserializeObject<dynamic>(s);
			StringBuilder sb = new StringBuilder();
			foreach (string item in result.Permissions) 
			{
				sb.AppendLine(item);
			}
			return sb.ToString();
		}

		private class RequestRoot
		{
			public APIRequest API_JSON_REQUEST = new();
		}
		private class APIRequest
		{
			public string Token = "";
			public string Type = "Status";
			public string Data = "";
		}
	}
}
