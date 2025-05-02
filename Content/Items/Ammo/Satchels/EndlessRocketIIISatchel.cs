using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using InfiniteAmmoPlus.Content.Utils;

namespace InfiniteAmmoPlus.Content.Items.Ammo.Satchels
{
    public class EndlessRocketIIISatchel : ModItem
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
                "RocketIII",                 // ammo item name
                width: 26,
                height: 34,
                rarity: ItemRarityID.Blue,      // rarity
                price: customPrice             // price
            );
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            AmmoUtils.AddRecipeIngredient(recipe, "RocketIII", 3996);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
