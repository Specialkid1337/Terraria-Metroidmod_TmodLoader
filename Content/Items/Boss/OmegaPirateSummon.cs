using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MetroidModPorted.Content.Items.Boss
{
	public class OmegaPirateSummon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Phazon Reactor Core");
			Tooltip.SetDefault("Summons the Omega Pirate");

			SacrificeTotal = 3;
		}
		public override void SetDefaults()
		{
			Item.maxStack = 20;
			Item.consumable = true;
			Item.width = 12;
			Item.height = 12;
			Item.useTime = 45;
			Item.useAnimation = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.noMelee = true;
			Item.value = 1000;
			Item.rare = ItemRarityID.Lime;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddRecipeGroup(MetroidModPorted.EvilBarRecipeGroupID, 5)
				.AddIngredient<Miscellaneous.PhazonBar>(1)
				.AddIngredient(ItemID.LunarTabletFragment, 1)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}

		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(ModContent.NPCType<NPCs.OmegaPirate.OmegaPirate>());
		}
		public override bool? UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<NPCs.OmegaPirate.OmegaPirate>());
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}
	}
}