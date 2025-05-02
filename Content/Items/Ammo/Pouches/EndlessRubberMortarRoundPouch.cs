using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using InfiniteAmmoPlus.Content.Utils;

namespace InfiniteAmmoPlus.Content.Items.Ammo.Pouches
{
    public class EndlessRubberMortarRoundPouch : ModItem
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
                "RubberMortarRound",                 // ammo item name
                width: 26,
                height: 34,
                rarity: ItemRarityID.Yellow,      // rarity
                price: customPrice,             // price
                modName: "CalamityMod"          // mod name
            );
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            AmmoUtils.AddRecipeIngredient(recipe, "RubberMortarRound", 3996, "CalamityMod");
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
