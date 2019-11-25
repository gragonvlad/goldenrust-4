using Oxide.Core.Libraries.Covalence;
using System.Collections.Generic;
using System;

namespace Oxide.Plugins
{
    [Info("First plugin", "Wasdik", "0.1.0")]
    [Description("Our first pugin for Rust")]
    class Wasdik7 : CovalencePlugin
    {
        private void Init()
        {
            
        }
		
		[Command("test11")]
		private void TestCommand11(IPlayer player, string command, string[] args)
		{

			//player.Reply("Test11 successful!, IsBanned: "+position.ToString());
			if (player.BelongsToGroup("admin"))
			{
				player.Reply("You are in the admin group");
			}
			else
			{
				player.Reply("You are NOT in the admin group");
			}
		}
		
		[Command("test12"), Permission("wasdik7.use")]
		private void TestCommand12(IPlayer player, string command, string[] args)
		{
			player.Reply("Test successful!");
		}
		
		[Command("test13")]
		private void TestCommand13(IPlayer player, string command, string[] args)
		{
			player.AddToGroup("admin");
		}
		
		[Command("test14")]
		private void TestCommand14(IPlayer player, string command, string[] args)
		{
			player.RemoveFromGroup("admin");
		}
		
    }
}