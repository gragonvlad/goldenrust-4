using Oxide.Core.Libraries.Covalence;

namespace Oxide.Plugins
{
    [Info("First plugin", "Wasdik", "0.1.0")]
    [Description("Our first pugin for Rust")]
    class Wasdik : CovalencePlugin
    {
        private void Init()
        {
            Puts("A baby plugin is born!");
        }

        protected override void LoadDefaultConfig()
		{
			LogWarning("Creating a new configuration file", "");
			Config["ShowJoinMessage"] = true;
			Config["ShowLeaveMessage"] = true;
			Config["JoinMessage"] = "Welcome to this server";
			Config["LeaveMessage"] = "Goodbye";
		}
		
		[Command("test")]
		private void TestCommand(IPlayer player, string command, string[] args)
		{
			Config["ShowJoinMessage"] = !(bool)Config["ShowJoinMessage"];
			Config["ShowLeaveMessage"] = !(bool)Config["ShowLeaveMessage"];
			SaveConfig();
		}
    }
}