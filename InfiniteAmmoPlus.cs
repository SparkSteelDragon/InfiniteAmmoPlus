using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace InfiniteAmmoPlus
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class InfiniteAmmoPlus : Mod
	{
		// You can use this method to set up your mod's content. This is where you can register items, tiles, NPCs, etc.
		public override void Load()
        {
            Logger.Info($"{Name} mod loaded successfully!");
        }

		public override void Unload()
		{
			// This is where you can clean up any resources your mod has loaded.
			// You can also reset any global variables or settings for your mod here.
		}
	}
}
