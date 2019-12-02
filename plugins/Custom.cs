using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Oxide.Plugins
{
    [Info("Custom", "31", "0.0.1")]
    public class Custom : RustPlugin
    {
        #region Classes
        public class DataConfig
        {
            [JsonProperty("Item Skin")]
            public ulong skinId;

            [JsonProperty("Item Name")]
            public string itemName;

            [JsonProperty("Item Description")]
            public string description;

            [JsonProperty("The chance of dropping an item in boxes")]
            public int Chance;

            [JsonProperty("Processing radiation")]
            public int radiation = 75;

            [JsonProperty("List of boxes in which to spawn it")]
            public List<string> ListContainers;
        }
        #endregion
        #region Config
        public DataConfig cfg;
        protected override void LoadConfig()
        {
            base.LoadConfig();
            cfg = Config.ReadObject<DataConfig>();
        }
        protected override void SaveConfig()
        {
            Config.WriteObject(cfg);
        }
        protected override void LoadDefaultConfig()
        {
            cfg = new DataConfig()
            {
                skinId = 1676503124,
                itemName = "<color=blue>Grandiy</color>",
                description = "<color=blue>Grandiy who in [Grand Rust]</color>",
                Chance = 100,
                radiation = 75,
                ListContainers = new List<string>()
                {
                    { "crate_basic" },
                    { "crate_elite" },
                    { "crate_normal" },
                    { "crate_normal_2" },
                    { "bradley_crate" },
                    { "heli_crate" },
                    { "loot-barrel-1" },
                    { "loot-barrel-2" }
                },
            };
        }
        #endregion
        #region OxideHooks[1]
        void OnLootSpawn(LootContainer container)
        {
            if (container.ShortPrefabName == "stocking_large_deployed" ||
                container.ShortPrefabName == "stocking_small_deployed") return;
            if (cfg.ListContainers.Contains(container.ShortPrefabName))
            {
                if (UnityEngine.Random.Range(0f, 100f) < cfg.Chance)
                {
                    if (container.inventory.itemList.Count == container.inventory.capacity)
                    {
                        container.inventory.capacity++;
                    }
                    Item i = ItemManager.CreateByName("coal", 1, cfg.skinId);
                    i.name += cfg.itemName += "\n";
                    i.MoveToContainer(container.inventory);
                }
            }
        }
        #endregion
        #region OxideHooks[2]
        private object OnRecycleItem(Recycler recycler, Item item)
        {
            {
                item.UseItem(1);
                switch (item.skin)
                {
                    case 1676503124:
                        recycler.MoveItemToOutput(ItemManager.CreateByName("sulfur", +Random.Range(250, 7250)));
                        break;
                    default:
                        return null;
                }
                return true;
            }
            return null;
        }
        void OnRecyclerToggle(Recycler recycler, BasePlayer player)
        {
            if (recycler.IsOn()) return;
            for (int i = 0; i < 6; i++)
            {
                Item slot = recycler.inventory.GetSlot(i);
                if (slot == null) continue;
                player.metabolism.radiation_poison.value = cfg.radiation;                     
            }
        }
    }
}
#endregion

