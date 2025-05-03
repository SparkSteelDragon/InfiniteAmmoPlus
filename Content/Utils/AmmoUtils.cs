using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace InfiniteAmmoPlus.Content.Utils
{
    public class AmmoOverrideEntry
    {
        public string WeaponFullName;
        public string ProjectileFullName;

        public AmmoOverrideEntry(string weaponFullName, string projectileFullName)
        {
            WeaponFullName = weaponFullName;
            ProjectileFullName = projectileFullName;
        }
    }

    public struct ItemPrice
    {
        public int gold;
        public int silver;
        public int copper;
        public int platinum;

        public ItemPrice(int gold, int silver, int copper, int platinum)
        {
            this.gold = gold;
            this.silver = silver;
            this.copper = copper;
            this.platinum = platinum;
        }

        public int ToCopper()
        {
            return platinum * 10000 + gold * 1000 + silver * 100 + copper;
        }
    }

    public static class AmmoUtils
    {
        private static readonly Dictionary<string, List<AmmoOverrideEntry>> AmmoSets = new()
        {
            ["RocketIII"] = new List<AmmoOverrideEntry>
            {
                new("Terraria:RocketLauncher", "Terraria:RocketIII"),
                new("Terraria:GrenadeLauncher", "Terraria:GrenadeIII"),
                new("Terraria:ProximityMineLauncher", "Terraria:ProximityMineIII"),
                new("Terraria:Celeb2", "Terraria:Celeb2RocketLarge"),
                new("Terraria:SnowmanCannon", "Terraria:RocketSnowmanIII"),
                new("CalamityMod:ShriekingLauncher", "CalamityMod:ShriekingRocket"),
            },
            ["ClusterI"] = new List<AmmoOverrideEntry>
            {
                new("Terraria:RocketLauncher", "Terraria:ClusterRocketI"),
                new("Terraria:GrenadeLauncher", "Terraria:ClusterGrenadeI"),
            }
        };

        private static readonly Dictionary<string, int?> ItemCache = new();
        private static readonly Dictionary<string, int?> ProjectileCache = new();

        public static bool TryOverrideProjectile(string ammoSetName, Item weapon, ref int projectileType)
        {
            if (AmmoSets.TryGetValue(ammoSetName, out var overrideSet))
            {
                foreach (var entry in overrideSet)
                {
                    int? weaponType = GetItemType(entry.WeaponFullName);
                    int? projectileTypeOverride = GetProjectileType(entry.ProjectileFullName);

                    if (weaponType.HasValue && projectileTypeOverride.HasValue && weaponType.Value == weapon.type)
                    {
                        projectileType = projectileTypeOverride.Value;
                        return true;
                    }
                }
            }
            return false;
        }

        private static int? GetItemType(string fullName)
        {
            if (ItemCache.TryGetValue(fullName, out int? cached))
            {
                return cached;
            }

            var parts = fullName.Split(':');
            if (parts.Length != 2)
            {
                ItemCache[fullName] = null;
                return null;
            }

            var modName = parts[0];
            var itemName = parts[1];

            int? result = null;

            if (modName == "Terraria")
            {
                int id = ItemID.Search.GetId(itemName);
                result = id >= 0 ? id : null;
            }
            else if (ModLoader.TryGetMod(modName, out Mod mod) && mod.TryFind(itemName, out ModItem modItem))
            {
                result = modItem.Type;
            }

            ItemCache[fullName] = result;
            return result;
        }

        private static int? GetProjectileType(string fullName)
        {
            if (ProjectileCache.TryGetValue(fullName, out int? cached))
            {
                return cached;
            }

            var parts = fullName.Split(':');
            if (parts.Length != 2)
            {
                ProjectileCache[fullName] = null;
                return null;
            }

            var modName = parts[0];
            var projName = parts[1];

            int? result = null;

            if (modName == "Terraria")
            {
                int id = ProjectileID.Search.GetId(projName);
                result = id >= 0 ? id : null;
            }
            else if (ModLoader.TryGetMod(modName, out Mod mod) && mod.TryFind(projName, out ModProjectile modProj))
            {
                result = modProj.Type;
            }

            ProjectileCache[fullName] = result;
            return result;
        }

        public static void AddRecipeIngredient(Recipe recipe, string itemName, int amount = 1, string modName = null)
        {
            Item sourceItem = null;

            if (string.IsNullOrEmpty(modName))
            {
                int vanillaType = ItemID.Search.GetId(itemName);
                if (vanillaType > 0)
                {
                    sourceItem = new Item();
                    sourceItem.SetDefaults(vanillaType);
                }
            }
            else if (ModLoader.TryGetMod(modName, out Mod mod))
            {
                if (mod.TryFind(itemName, out ModItem modItem))
                {
                    sourceItem = new Item();
                    sourceItem.SetDefaults(modItem.Type);
                }
                else
                {
                    return;
                }
            }

            if (sourceItem != null)
            {
                recipe.AddIngredient(sourceItem.type, amount);
            }
        }

        public static void SetAmmoDefaults(
            ModItem modItem,
            string ammoItemName,
            int width,
            int height,
            int rarity,
            ItemPrice? price = null,
            string modName = null
        )
        {
            Item item = modItem.Item;
            Item sourceItem = null;

            if (string.IsNullOrEmpty(modName))
            {
                int vanillaType = ItemID.Search.GetId(ammoItemName);
                if (vanillaType > 0)
                {
                    sourceItem = new Item();
                    sourceItem.SetDefaults(vanillaType);
                }
            }
            else if (ModLoader.TryGetMod(modName, out Mod mod))
            {
                if (mod.TryFind(ammoItemName, out ModItem ammoItem))
                {
                    sourceItem = new Item();
                    sourceItem.SetDefaults(ammoItem.Type);
                }
            }

            if (sourceItem == null)
            {
                modItem.Item.TurnToAir();
                return;
            }

            item.shootSpeed = sourceItem.shootSpeed;
            item.shoot = sourceItem.shoot;
            item.damage = sourceItem.damage;
            item.ammo = sourceItem.ammo;
            item.knockBack = sourceItem.knockBack;
            item.DamageType = sourceItem.DamageType;

            item.width = width;
            item.height = height;

            int finalPrice = price.HasValue ? price.Value.ToCopper() : Item.sellPrice(silver: 2);
            item.value = finalPrice;
            item.rare = rarity;
        }
    }
}
