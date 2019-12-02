using System.Collections.Generic;
using UnityEngine;
using System;
using Rust;
using Oxide.Core;
using Oxide.Core.Plugins;
namespace Oxide.Plugins
{
    [Info("Test", "31", "0.0.1")]
    public class Test : RustPlugin
    {
        //[ChatCommand("spawn")]
        void OnMeleeThrown(BasePlayer player, Item item)
        {
            Vector3 charpos = player.transform.position;
            charpos.x = 958.5f;
            charpos.y = 49.6f;
            charpos.z = -1429.6f;
            Effect.server.Run("assets/prefabs/tools/c4/effects/c4_explosion.prefab", charpos);
        } 
    }
}



























































































