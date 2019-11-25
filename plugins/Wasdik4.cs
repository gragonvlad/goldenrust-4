using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Configuration;
using Oxide.Core;
using System.Collections.Generic;

namespace Oxide.Plugins
{
    [Info("First plugin", "Wasdik", "0.1.0")]
    [Description("Our first pugin for Rust")]
    class Wasdik4 : CovalencePlugin
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
		
		private DataFileSystem dataFile;

		private void Init()
		{
			storedData = Interface.Oxide.DataFileSystem.ReadObject<StoredData>("MyDataFile");
			Interface.Oxide.DataFileSystem.WriteObject("MyDataFile", storedData);
			
			dataFile = new DataFileSystem($"{Interface.Oxide.DataDirectory}\\player_info");
		}
		
		private void SavePlayerInfo(string playerId, PlayerInfo playerInfo)
		{
			dataFile.WriteObject<PlayerInfo>($"playerInfo_{playerId}", playerInfo);
		}
		
		private PlayerInfo LoadPlayerInfo(string playerId)
		{
			return dataFile.ReadObject<PlayerInfo>($"playerInfo_{playerId}");
		}
		
		[Command("test7")]
		private void TestCommand7(IPlayer player, string command, string[] args)
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
		
		[Command("test8")]
		private void TestCommand8(IPlayer player, string command, string[] args)
		{
			PlayerInfo info = new PlayerInfo(player);
			SavePlayerInfo(player.Id, info);
			player.Reply("Saving your data to the file...");
		}
		
		[Command("test9")]
		private void TestCommand9(IPlayer player, string command, string[] args)
		{
			PlayerInfo info = LoadPlayerInfo(player.Id);
			if (info == null)
				player.Reply("Not your data to the file...");
			else
				player.Reply("info.Name: "+info.Name);
		}
    }
}