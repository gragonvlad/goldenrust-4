using Oxide.Core.Libraries.Covalence;
using System.Collections.Generic;
using System;
using Oxide.Core.Libraries;

namespace Oxide.Plugins
{
    [Info("First plugin", "Wasdik", "0.1.0")]
    [Description("Our first pugin for Rust")]
    class Wasdik8 : CovalencePlugin
    {
        private void Init()
        {
            permission.RegisterPermission("wasdik8.use", this);  
        }
		
		
		[Command("testperm")]
		private void PostRequest(IPlayer player, string command, string[] args)
		{
			bool GroupCreated = permission.CreateGroup("VIP", "Group VIP", 0);
			Puts("GroupCreated: "+GroupCreated.ToString());
		}
		
    }
}