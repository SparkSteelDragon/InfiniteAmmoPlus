using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using InfiniteAmmoPlus.Content.Utils;

namespace InfiniteAmmoPlus.Content.Items.Ammo.Satchels
{
    public class EndlessRocketISatchel : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            ItemPrice customPrice = new ItemPrice(0, 20, 0, 0); // gold, silver, copper, platinum
            AmmoUtils.SetAmmoDefaults(
                this,
                "RocketI",                 // ammo item name
                width: 26,
                height: 34,
                rarity: ItemRarityID.White,      // rarity
                price: customPrice             // price
            );
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            AmmoUtils.AddRecipeIngredient(recipe, "RocketI", 3996);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }


        public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref StatModifier damage, ref float knockback)
        {
            AmmoUtils.TryOverrideProjectile("RocketI", weapon, ref type);
        }
    }
}
