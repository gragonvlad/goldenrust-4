using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Oxide.Plugins
{
    [Info("GoldenSkull", "31", "0.0.1")]
    class GoldenSkull : RustPlugin
    {		
		private void OnServerInitialized() => LoadConfig();
		
        void OnItemAddedToContainer(ItemContainer container, Item item)
        {
            if (item != null && item.info.shortname == "skull.human" && item.name != _conf.Settings.name && item.skin == _conf.Settings.skin) item.name = _conf.Settings.name;
        }
		
		object OnDispenserGather(ResourceDispenser dispenser, BaseEntity entity, Item item)
		{
			if (entity is BasePlayer && item.info.shortname == "skull.human")
            {
				if (_conf.Settings.chance >= UnityEngine.Random.Range(0, 100))
				{
					BasePlayer player = entity as BasePlayer;
					
					PrintToChat(player, $"Вы нашли <color=#ffd700>золотой череп</color>!\nИзмельчите его и получите ресурсы.");
					
					item.skin = _conf.Settings.skin;
				}
            }
			return null;
		}
		
		object OnItemAction(Item item, string action, BasePlayer player)
		{
			if (item.info.shortname == "skull.human" && item.skin == _conf.Settings.skin)
            { 
				if (action.Equals("crush"))
				{
					var ritem = _conf.itemlist.GetRandom();
					var amount = UnityEngine.Random.Range(ritem.min, ritem.max);
					Item golden = ItemManager.CreateByName(ritem.shortname, amount);
					if (24 - player.inventory.containerMain.itemList.Count > 0)
					{
						golden.MoveToContainer(player.inventory.containerMain);
					}
					else if (6 - player.inventory.containerBelt.itemList.Count > 0)
					{
						golden.MoveToContainer(player.inventory.containerBelt);
					}
					else
					{
						golden.Drop(player.transform.position, Vector3.up);
						PrintToChat(player, "Ресурсы брошены Вам под ноги!");
					}
					PrintToChat($"Вы раздробили <color=#ffd700>золотой череп</color> и получили {golden.info.displayName.english} ({amount} шт.)");
				}
            }
			return null;
		}
		
		private _Conf _conf; 
		
        class _Conf
        {          
            [JsonProperty(PropertyName = "Настройки")]
            public Options Settings { get; set; }
            [JsonProperty(PropertyName = "Список предметов")]
            public List<ItemConfig> itemlist { get; set; }

			public class ItemConfig
			{
				[JsonProperty(PropertyName = "Shortname предмета")]
				public string shortname { get; set; }
				[JsonProperty(PropertyName = "Минимальное количество")]
				public int min { get; set; }
				[JsonProperty(PropertyName = "Максимальное количество")]
				public int max { get; set; }
			}
			
            public class Options
            {
                [JsonProperty(PropertyName = "Шанс того, что при добыче черепа он заменится на золотой")]
                public int chance { get; set; }
                [JsonProperty(PropertyName = "Новое название предмета")]
                public string name { get; set; }
                [JsonProperty(PropertyName = "Новый скин предмета")]
                public ulong skin { get; set; }
            }
        }
		
        protected override void LoadConfig()
        {
            base.LoadConfig();
            _conf = Config.ReadObject<_Conf>();

            Config.WriteObject(_conf, true);
        }

        protected override void LoadDefaultConfig()
		{
			_conf = SetDefaultConfig();
			PrintWarning("Creating config file...");
		}

        private _Conf SetDefaultConfig()
        {
            return new _Conf
            {
				itemlist = new List<_Conf.ItemConfig>()
                {
					new _Conf.ItemConfig {shortname = "sulfur", max = 1250, min = 2500 },
					new _Conf.ItemConfig {shortname = "stones", max = 2500, min = 5000 },
					new _Conf.ItemConfig {shortname = "wood", max = 2500, min = 5000 },
					new _Conf.ItemConfig {shortname = "metal.fragments", max = 1250, min = 2500 }
                },
                Settings = new _Conf.Options
                {
                    chance = 15,
                    name = "Золотой череп",
                    skin = 1492610636
                }              
            };
        }

        protected override void SaveConfig() => Config.WriteObject(_conf, true);

        private void UpdateConfigValues()
        {
            PrintWarning("Updating config file...");

            _Conf baseConfig = SetDefaultConfig();
        }
    }
}