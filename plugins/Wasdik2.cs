using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Configuration;
using Oxide.Core;

namespace Oxide.Plugins
{
    [Info("First plugin", "Wasdik", "0.1.0")]
    [Description("Our first pugin for Rust")]
    class Wasdik2 : CovalencePlugin
    {
		private class PluginConfig
		{
			public bool ShowJoinMessage;
			public bool ShowLeaveMessage;
			public string JoinMessage;
			public string LeaveMessage;
		}
		
		private PluginConfig config;
		private DynamicConfigFile dataFile;
		
        private void Init()
        {
            config = Config.ReadObject<PluginConfig>();
			
			dataFile = Interface.Oxide.DataFileSystem.GetDatafile("Wasdik2");

			dataFile["EpicString"] = "EpicValue";
			dataFile["EpicNumber"] = 42;
			dataFile["EpicCategory", "EpicString2"] = "EpicValue2";

			dataFile.Save();
        }

        protected override void LoadDefaultConfig()
		{
			Config.WriteObject(GetDefaultConfig(), true);
		}
		
		private PluginConfig GetDefaultConfig()
		{
			return new PluginConfig
			{
				ShowJoinMessage = true,
				ShowLeaveMessage = true,
				JoinMessage = "Welcome to this server",
				LeaveMessage = "Goodbye"
			};
		}
		
		[Command("test2")]
		private void TestCommand2(IPlayer player, string command, string[] args)
		{
			// Check if the EpicString exists
			if (dataFile["EpicString"] != null)
			{
				Puts(dataFile["EpicString"].ToString()); // Outputs: EpicValue
			}

			// Check if the EpicNumber exists
			if (dataFile["EpicNumber"] != null)
			{
				Puts(dataFile["EpicNumber"].ToString()); // Outputs: 42
			}
		}
		
		[Command("test3")]
		private void TestCommand3(IPlayer player, string command, string[] args)
		{
			dataFile.Remove("EpicString");
			dataFile.Save();
		}
		
		[Command("test4")]
		private void TestCommand4(IPlayer player, string command, string[] args)
		{
			dataFile.Clear();
			dataFile.Save();
		}
		
		[Command("test5")]
		private void TestCommand5(IPlayer player, string command, string[] args)
		{
			if (Interface.Oxide.DataFileSystem.ExistsDatafile("Wasdik2"))
			{
				Puts("MyDataFile exists!");
			}
			else
			{
				Puts("MyDataFile does not exist");
			}
		}
    }
}