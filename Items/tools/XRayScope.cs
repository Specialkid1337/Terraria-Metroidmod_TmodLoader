using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MetroidMod.Items.tools
{
	public class XRayScope : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "X-Ray Scope";
			item.maxStack = 1;
			item.width = 20;
			item.height = 20;
			item.toolTip = "Projects a wide ray of light if you are standing still";
			item.noUseGraphic = true;
			item.useStyle = 5;
			item.useTime = 2;
			item.useAnimation = 2;
			item.autoReuse = true;
			item.value = 400000;
			item.rare = 5;
			item.channel = true;
		}
        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod); 
            recipe.AddIngredient("Cobalt Bar", 10);
            recipe.AddIngredient("Spelunker Potion");
            recipe.AddIngredient("Glowing Mushroom", 30); 
            recipe.AddTile(TileID.Anvils);   
            recipe.SetResult(this);
            recipe.AddRecipe();
			recipe = new ModRecipe(mod); 
            recipe.AddIngredient("Palladium Bar", 10);
            recipe.AddIngredient("Spelunker Potion");
            recipe.AddIngredient("Glowing Mushroom", 30); 
            recipe.AddTile(TileID.Anvils);   
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
		/*public static XRayScope xray = new XRayScope();
		public override void Initialize()
		{
			xray = this;
		}*/
		public override bool UseItem(Player player)
		{
			MPlayer mp = player.GetModPlayer<MPlayer>(mod);
			//mp.xrayequipped = true;
			int range = 16;
			float MY = Main.mouseY + Main.screenPosition.Y;
			float MX = Main.mouseX + Main.screenPosition.X;
			if (player.gravDir == -1f)
			{
				MY = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
			}
			float targetrotation = (float)Math.Atan2((MY-player.Center.Y),(MX-player.Center.X));
			if (player.velocity.Y == 0 && player.velocity.X == 0)
			{
			for(int i = 0; i < 20; i++)
			{
				Vector2 lightPos = new Vector2(player.Center.X+(float)Math.Cos(targetrotation)*range*(2+(2*i)),player.position.Y+(float)Math.Sin(targetrotation)*range*(2+(2*i))+8);
				if(!player.dead && !mp.ballstate && player.velocity.Y == 0 && player.velocity.X == 0)
				{
					if ((Main.mouseX + Main.screenPosition.X) > player.position.X)
					{
						player.direction = 1;
					}
					if ((Main.mouseX + Main.screenPosition.X) < player.position.X)
					{
						player.direction = -1;
					}
					Lighting.AddLight((int)((float)lightPos.X/16f), (int)((float)lightPos.Y/16f), 0.75f+(0.25f*i), 0.75f+(0.25f*i), 0.75f+(0.25f*i));
				}
			}
			}
			return true;
		}
	/*	public bool XRayActive(Player player)
		{
			MPlayer mp = player.GetModPlayer<MPlayer>(mod);
			if(MBase.KeyPressed(MBase.XRayKey) && !mp.ballstate && player.velocity.Y == 0 && player.velocity.X == 0 && player.itemAnimation == 0)
			{
				return true;
			}
			return false;
		}*/
	}
}