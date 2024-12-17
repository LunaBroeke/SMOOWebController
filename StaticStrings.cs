using SMOOWebController.Backend;
using System.Net;
using System.Net.NetworkInformation;

namespace SMOOWebController
{
	public static class StaticStrings
	{
		public static string GetLocalAddress()
		{
			return Settings.Instance.httpSender.address;
		}
	}
}
