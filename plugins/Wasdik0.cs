using Oxide.Core.Libraries.Covalence;
using System.Collections.Generic;

namespace Oxide.Plugins
{
    [Info("First plugin", "Wasdik", "0.1.0")]
    [Description("Our first pugin for Rust")] 
	//class Wasdik6 : CovalencePlugin
    class Wasdik0 : RustPlugin
    {
        private void Init()
        {
			// Server.Broadcast
        }
		
		[ChatCommand("test0")]
		private void TestUpdateCommand(BasePlayer player, string command, string[] args)
		{
			
			switch (args.Length)
            {
                case 1:
					string item_shortname = args[0];
					Item item = ItemManager.CreateByName(item_shortname, 1);
					if (item==null) break;
					//item.info.displayDescription = "Best sticks!!!!";
					//item.dirty = true;
					Puts("-----item.info.displayDescription.english-----: "+item.info.displayDescription.english);
					Puts("-----item.text-----: "+item.text);
					item.name = item.info.displayName.english + " (golden)";
					player.GiveItem(item, BaseEntity.GiveItemReason.PickedUp);
                    break;
                default:
					Puts("-----test0-----");
                    break;
            }
		}
		
    }
}