namespace SMOOWebController.Backend
{
	public class Settings
	{
		public string address = "127.0.0.1";
		public int port = 1027;
		public string token = "localtoken";
		public static Settings LoadSettings()
		{
			return new Settings();
		}
	}
}
