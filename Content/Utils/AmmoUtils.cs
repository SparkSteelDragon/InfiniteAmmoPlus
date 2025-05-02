using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using System;
using InfiniteAmmoPlus.Content.Utils;

namespace InfiniteAmmoPlus.Content.Utils
{
    public struct ItemPrice
    {
        public int gold;
        public int silver;
        public int copper;
        public int platinum;

        // constructor for ItemPrice
        public ItemPrice(int gold, int silver, int copper, int platinum)
        {
            this.gold = gold;
            this.silver = silver;
            this.copper = copper;
            this.platinum = platinum;
        }

        // Method to convert coins in copper to ItemPrice
        public int ToCopper()
        {
            return platinum * 10000 + gold * 1000 + silver * 100 + copper;
        }
    }

    public static class AmmoUtils
    {
        // This method is used to add an ingredient to a recipe for a mod item.
        public static void AddRecipeIngredient(Recipe recipe, string itemName, int amount = 1, string modName = null)
        {
            Item sourceItem = null;

            // search for the item in the mod or vanilla items
            if (string.IsNullOrEmpty(modName))
            {
                // search in vanilla items
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
                    return; // if the item is not found in the mod, skip
                }
            }

            if (sourceItem != null)
            {
                recipe.AddIngredient(sourceItem.type, amount); // add the ingredient to the recipe
            }
            else
            {
                return;
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
                // search in vanilla items
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
                modItem.Item.TurnToAir(); // if the source item is not found, remove the item
                return;
            }

            // copy properties from the source item
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
