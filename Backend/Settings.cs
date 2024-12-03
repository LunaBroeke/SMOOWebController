using Newtonsoft.Json;

namespace SMOOWebController.Backend
{
	public class Settings
	{
		public SMOOServer smooServer = new SMOOServer();
		public TokenSettings tokenSettings = new TokenSettings();
		public StartButton startButton = new StartButton();
		public LogListener logListener = new LogListener();
		private const string path = "websettings.json";
		[JsonIgnore] public string token = "localtoken"; //SMOO Server JsonAPI token
		/// <summary>
		/// Settings for connecting to the Super Mario Odyssey Online Server
		/// </summary>
		public class SMOOServer
		{
			public string Address = "127.0.0.1"; // SMOO Server Address
			public int Port = 1027; // SMOO Server Port
		}
		/// <summary>
		/// Settings for a start button functionality
		/// </summary>
		public class StartButton
		{
			public bool Enabled = false;
			public string ServerPath = ".../Server.exe"; // Physical Path to your Server.exe
		}
		/// <summary>
		/// Listener for a special build of SMOO that can post it's logs to this web app (https://github.com/LunaBroeke/SmoOnlineServer)
		/// </summary>
		public class LogListener
		{
			public bool Enabled = false;
			public string Address = "0.0.0.0"; // Listening address
			public int Port = 11027; // Listening port
		}
		/// <summary>
		/// Settings for Authorization via Discord
		/// </summary>
		public class TokenSettings
		{
			public ulong DiscordGuildID = 481991664085499924; // Discord Server ID where it should be checking. Default is set to the CraftyBoss community server
			public ulong DiscordRoleID = 509882683195916289; // Discord Role the server will be checking if the user has it. Default is set to the Helper role in CraftyBoss community server
			public List<TokenAuth> Authorizations = new List<TokenAuth>();
			public string Default = "localtoken"; // Default token for if the User ID check in Authorizations fails but still passed the role check.
		}
		/// <summary>
		/// Per user authorization. Role check will have to be passed first.
		/// </summary>
		public class TokenAuth
		{
			public string Name = "Luwuna"; //This doesn't need to be applied in code. This is just for organization
			public ulong DiscordUserID = 628251183420801026; //Discord User ID. Default is set to me, so if you don't want me to have access; best you remove this example.
			public string Token = "localtoken"; //User specific token.
		}
		/// <summary>
		/// Function that loads the settings, this is only supposed to be called ONCE in JsonAPI.cs
		/// </summary>
		/// <returns></returns>
		public static Settings LoadSettings()
		{
			Settings settings = null;
			if (File.Exists(path))
			{
				string json = File.ReadAllText(path);
				settings = JsonConvert.DeserializeObject<Settings>(json);
			}
			else
			{
				Program.logger.Log($"{path} not found, creating new");
				settings = new Settings();
				settings.tokenSettings.Authorizations.Add(new TokenAuth());
			}
			//settings.address = settings.smooServer.Address;
			//settings.port = settings.smooServer.port;
			settings.token = GetToken(settings);
			settings.SaveSettings();
			return settings;
		}
		/// <summary>
		/// Authorization function that gets the logged in users specific token.
		/// </summary>
		/// <param name="settings"></param>
		/// <returns>JsonAPI token</returns>
		private static string GetToken(Settings settings)
		{
			// Logic for getting the token, Until we have authorizaiton figured out we will leave this to a default value
			return settings.tokenSettings.Default;
		}
		/// <summary>
		/// Save Settings/Settings Refresh
		/// </summary>
		public void SaveSettings()
		{
			string json = JsonConvert.SerializeObject(this,Formatting.Indented);
			File.WriteAllText(path, json);
			Program.logger.Log($"Written settings to {Path.GetFullPath(path)}");
		}
	}
}
