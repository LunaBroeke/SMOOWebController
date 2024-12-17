using System.Net.Sockets;
using System.Text;

namespace SMOOWebController.Backend
{
	public class LogListener
	{
		public LogListener(int port)
		{
			this.port = port;
		}
		public string path = "wwwroot/log.txt";
		public readonly int port;
		public static Action<string> Log;
		public async Task Start()
		{
			Log += AddString;
			try
			{
				using (UdpClient udpClient = new UdpClient(port))
				{
					Console.WriteLine($"Listening on Port {port}");
					while (true)
					{
						UdpReceiveResult result = await udpClient.ReceiveAsync();
						string message = Encoding.UTF8.GetString(result.Buffer);
						Console.WriteLine(message);
						Log.Invoke(message);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error " + ex.Message);
			}
		}

		public void AddString(string str)
		{

		}
	}
}
