namespace SMOOWebController
{
	public class Program
	{
		public static Logger logger = new Logger() { name = "console" };
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorPages();
			builder.Services.AddControllers();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			//app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();
			app.MapControllers();
			app.MapRazorPages();
			Backend.JsonAPI apiDebug = new Backend.JsonAPI("localtoken");
			Thread manualCommands = new Thread(() => { while (true) { logger.Log(apiDebug.SendCommand(Console.ReadLine())); } }) { IsBackground = true };
			logger.Log(apiDebug.SendCommand("help"));
			logger.Log(apiDebug.GetPermissions());
			manualCommands.Start();

			app.Run();
		}
	}
}