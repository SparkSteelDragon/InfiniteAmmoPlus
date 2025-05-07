using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using InfiniteAmmoPlus.Content.Utils;

namespace InfiniteAmmoPlus.Content.Items.Ammo.Satchels
{
    public class EndlessClusterRocketISatchel : ModItem
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
                "ClusterRocketI",                 // ammo item name
                width: 26,
                height: 34,
                rarity: ItemRarityID.Blue,      // rarity
                price: customPrice             // price
            );
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            AmmoUtils.AddRecipeIngredient(recipe, "ClusterRocketI", 3996);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }


        public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref StatModifier damage, ref float knockback)
        {
            AmmoUtils.TryOverrideProjectile("ClusterRocketI", weapon, ref type);
        }
    }
}
