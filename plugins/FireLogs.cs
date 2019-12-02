using Oxide.Core.Libraries.Covalence;
using System.Collections.Generic;
using System;
using Oxide.Core.Libraries;
using Oxide.Game.Rust.Cui;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("FireLogs", "Wasdik", "0.1.0")]
    [Description("Logs fire")]
    class FireLogs : RustPlugin
    {
        private void Init()
        {
            
        }
			
		void OnWeaponFired(BaseProjectile projectile, BasePlayer player, ItemModProjectile mod, ProtoBuf.ProjectileShoot projectiles)
		{
			Puts("------------START!-----------");
			Puts("projectile: "+projectile.ToString());
			Puts("projectile.NoiseRadius: "+projectile.NoiseRadius.ToString());
			Puts("------------------------------");
			Puts("player: "+player.ToString());
			Puts("------------------------------");
			Puts("mod: "+mod.ToString());
			Puts("mod.projectileObject: "+mod.projectileObject.ToString());
			Puts("mod.numProjectiles: "+mod.numProjectiles.ToString());
			Puts("mod.projectileVelocity: "+mod.projectileVelocity.ToString());
			Puts("mod.category: "+mod.category);
			Puts("mod.mods: "+mod.mods.ToString());
			Puts("mod.ammoType: "+mod.ammoType.ToString());
			Puts("mod.projectileSpread: "+mod.projectileSpread.ToString());
			Puts("mod.projectileVelocitySpread: "+mod.projectileVelocitySpread.ToString());
			Puts("mod.useCurve: "+mod.useCurve.ToString());
			Puts("mod.spreadScalar: "+mod.spreadScalar.ToString());
			Puts("mod.attackEffectOverride: "+mod.attackEffectOverride.ToString());
			Puts("mod.barrelConditionLoss: "+mod.barrelConditionLoss.ToString());
			Puts("------------------------------");
			Puts("projectiles: "+projectiles.ToString());
			Puts("projectiles.ammoType: "+projectiles.ammoType.ToString());
			Puts("------------END!-----------");
			
		}
		
    }
}