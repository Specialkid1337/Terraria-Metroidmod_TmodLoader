using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MetroidMod.Content.Items.Boss
{
	public class GoldenTorizoBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

			ItemID.Sets.BossBag[Type] = true;
			SacrificeTotal = 3;
		}
		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			Item.expert = true;
			Item.rare = -12;
		}

		public override bool CanRightClick() => true;
		public override int BossBagNPC => ModContent.NPCType<NPCs.GoldenTorizo.GoldenTorizo>();

		public override void OpenBossBag(Player player)
		{
			player.QuickSpawnItem(player.GetSource_OpenItem(Type), SuitAddonLoader.GetAddon<SuitAddons.ScrewAttack>().ItemType);
			if (Main.rand.NextBool(2))
			{
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<Tiles.TorizoMusicBox>());
			}
			/*if (Main.rand.NextBool(3))
			{
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<Vanity.GoldenTorizoMask>());
			}
			if (Main.rand.NextBool(5))
			{
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<Tiles.GoldenTorizoTrophy"));
			}*/
		}
	}
}

