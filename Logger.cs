using System.Drawing;

namespace SMOOWebController
{
	public class Logger
	{
		public string name = "Logger"; // Change this name to wherever you're creating a new Logger
		public void Log(string message)
		{
			WriteLog(message, "", ConsoleColor.White);
		}
		public void Warn(string message)
		{
			WriteLog(message, "Warning: ", ConsoleColor.Yellow);
		}
		public void Error(string message)
		{
			WriteLog(message, "Error: ", ConsoleColor.Red);
		}
		public void Error(Exception exception)
		{
			WriteLog(exception.ToString(), "Exception: ", ConsoleColor.Red);
		}
		private void WriteLog(string message, string prefix, ConsoleColor color)
		{
			string time = DateTime.Now.ToString("HH:mm:ss.ff");
			Console.ForegroundColor = color;
			Console.WriteLine($"[{time}][{name}]{prefix}{message}");
		}
	}
}
