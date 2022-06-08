using Terraria.ID;
using Terraria.ModLoader;
using MetroidModPorted.Common.GlobalItems;

namespace MetroidModPorted.Content.Items.MissileAddons
{
	public class SeekerMissileAddon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Seeker Missile");
			Tooltip.SetDefault(string.Format("[c/9696FF:Missile Launcher Addon]\n") +
			"Slot Type: Charge\n" +
			"Fires missiles at multiple targets simultaneously\n" + 
			"Hold click to lock on to targets, and release to fire\n" + 
			"Can lock on to a maximum of 5 targets\n" + 
			"Consumes the appropriate number of missiles");

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
			Item.createTile = mod.TileType("SeekerMissileTile");*/
			MGlobalItem mItem = Item.GetGlobalItem<MGlobalItem>();
			mItem.missileSlotType = 0;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddRecipeGroup(MetroidModPorted.T1HMBarRecipeGroupID, 10)
				.AddIngredient(ItemID.SoulofNight, 1)
				.AddIngredient(ItemID.SoulofLight, 1)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
