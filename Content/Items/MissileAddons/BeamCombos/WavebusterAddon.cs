using Terraria.ID;
using Terraria.ModLoader;
using MetroidModPorted.Common.GlobalItems;

namespace MetroidModPorted.Content.Items.MissileAddons.BeamCombos
{
	public class WavebusterAddon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wavebuster");
			Tooltip.SetDefault(string.Format("[c/9696FF:Missile Launcher Addon]\n") +
			"Slot Type: Charge\n" +
			"Hold Click to charge\n" + 
			"~Fires a constant stream of electricity at full charge\n" + 
			"~Initially costs 10 missiles, then drains 5 missiles per second during use");

			SacrificeTotal = 1;
		}
		public override void SetDefaults()
		{
			Item.width = 10;
			Item.height = 14;
			Item.maxStack = 1;
			Item.value = 2500;
			Item.rare = 4;
			/*Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.createTile = mod.TileType("WavebusterTile");*/
			MGlobalItem mItem = Item.GetGlobalItem<MGlobalItem>();
			mItem.missileSlotType = 0;
			mItem.addonChargeDmg = 1f;
			mItem.addonMissileCost = 10;
			mItem.addonMissileDrain = 5;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddRecipeGroup(MetroidModPorted.T2HMBarRecipeGroupID, 10)
				.AddIngredient(ItemID.SoulofNight, 1)
				.AddIngredient(ItemID.Amethyst, 1)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
