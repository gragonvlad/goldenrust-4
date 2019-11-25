using Oxide.Core.Libraries.Covalence;
using System.Collections.Generic;

namespace Oxide.Plugins
{
    [Info("First plugin", "Wasdik", "0.1.0")]
    [Description("Our first pugin for Rust")] 
	//class Wasdik6 : CovalencePlugin
    class Wasdik6 : RustPlugin
    {
        private void Init()
        {
			// Server.Broadcast
        }
		
		object CanChangeCode(BasePlayer player, CodeLock codeLock, string newCode, bool isGuestCode)
		{
			if (newCode == "1111")
			{
				Server.Broadcast("Слишком простой пароль! "+newCode);
				return false;
			}
			Server.Broadcast("CanChangeCode works!");
			return null;
		}
    }
}