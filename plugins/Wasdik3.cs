using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Configuration;
using Oxide.Core;
using System.Collections.Generic;

namespace Oxide.Plugins
{
    [Info("First plugin", "Wasdik", "0.1.0")]
    [Description("Our first pugin for Rust")]
    class Wasdik3 : CovalencePlugin
    {
		private class StoredData
		{
			public HashSet<PlayerInfo> Players = new HashSet<PlayerInfo>();

			public StoredData()
			{
			}
		}

		private class PlayerInfo
		{
			public string Id;
			public string Name;

			public PlayerInfo()
			{
			}

			public PlayerInfo(IPlayer player)
			{
				Id = player.Id;
				Name = player.Name;
			}
		}

		private StoredData storedData;

		private void Init()
		{
			storedData = Interface.Oxide.DataFileSystem.ReadObject<StoredData>("MyDataFile");
			Interface.Oxide.DataFileSystem.WriteObject("MyDataFile", storedData);
		}
		
		[Command("test6")]
		private void TestCommand(IPlayer player, string command, string[] args)
		{
			PlayerInfo info = new PlayerInfo(player);
			storedData = Interface.Oxide.DataFileSystem.ReadObject<StoredData>("MyDataFile");
			bool result = storedData.Players.Contains(info);
			Puts(result.ToString());
			if (result)
			{
				player.Reply("Your data has already been added to the file");
			}
			else
			{
				storedData.Players.Add(info);
				player.Reply("Saving your data to the file...");
				Interface.Oxide.DataFileSystem.WriteObject("MyDataFile", storedData);
			}
			bool result2 = storedData.Players.Contains(info);
			Puts(result2.ToString());
		}
    }
}