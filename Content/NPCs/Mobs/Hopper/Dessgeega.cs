﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace MetroidMod.Content.NPCs.Mobs.Hopper
{
	public class Dessgeega : MNPC
	{
		private bool spawn = false;
		private float newScale = -1;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dessgeega");
			Main.npcFrameCount[NPC.type] = 3;
			NPCID.Sets.MPAllowedEnemies[Type] = true;

			NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
			{
				SpecificallyImmuneTo = new int[] {
					BuffID.OnFire,
					BuffID.CursedInferno
				}
			};
			NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("A creature capable of bouncing on walls. But it is much stronger than the Sidehopper. They are more commonly found in hotter places… As with the Sidehopper, they are able to come in various sizes.")
			});
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return (spawnInfo.SpawnTileY > WorldGen.lavaLine ? SpawnCondition.Cavern.Chance * 0.05f : 0) + SpawnCondition.Underworld.Chance * 0.1f;
		}
		
		public override void SetDefaults()
		{
			NPC.width = 60;
			NPC.height = 50;
			NPC.aiStyle = -1;
			NPC.damage = 30;
			NPC.defense = 16;
			NPC.lifeMax = 70;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = Item.buyPrice(0, 0, 3, 50);
			NPC.knockBackResist = 0.35f;
			//banner = NPC.type;
			//bannerItem = mod.ItemType("DessgeegaBanner");
			NPC.noGravity = true;
			NPC.behindTiles = true;
			NPC.lavaImmune = true;

			/* NPC scale networking fix. */
			if (Main.rand != null && Main.netMode != NetmodeID.MultiplayerClient)
				newScale = (Main.rand.Next(9, 12) * 0.1f);
		}
		private void SetStats()
		{
			NPC.scale = newScale;
			NPC.position.X += (float)(NPC.width / 2);
			NPC.position.Y += (float)(NPC.height);
			NPC.width = (int)((float)NPC.width * NPC.scale);
			NPC.height = (int)((float)NPC.height * NPC.scale);
			NPC.position.X -= (float)(NPC.width / 2);
			NPC.position.Y -= (float)(NPC.height);
			NPC.defense = (int)((float)NPC.defense * NPC.scale);
			NPC.damage = (int)((float)NPC.damage * NPC.scale);
			NPC.life = (int)((float)NPC.life * NPC.scale);
			NPC.lifeMax = NPC.life;
			NPC.value = (float)((int)(NPC.value * NPC.scale));
			NPC.npcSlots *= NPC.scale;
			NPC.knockBackResist *= 2f - NPC.scale;
		}
		
		public override bool PreAI()
		{
			if (!spawn && newScale != -1)
			{
				SetStats();
				spawn = true;
				NPC.netUpdate = true;
			}
			return true;
		}

		public override void AI()
		{
			mNPC.HopperAI(NPC, 5f, 8f);
			
			if(NPC.ai[1] == 1f)
			{
				if(NPC.ai[0] < 30)
				{
					if(NPC.frameCounter > 0)
					{
						NPC.frameCounter--;
					}
				}
				else
				{
					NPC.frameCounter = 10;
				}
				if(NPC.frameCounter > 0)
				{
					NPC.frame.Y = 1;
				}
				else
				{
					NPC.frame.Y = 0;
				}
			}
			else
			{
				NPC.frameCounter = 10;
				NPC.frame.Y = 2;
			}
		}
		public override bool PreDraw(SpriteBatch sb, Vector2 screenPos, Color drawColor)
		{
			mNPC.DrawHopper(NPC,sb,screenPos,drawColor);
			return false;
		}
		
		Vector2 RandomVel => new Vector2(Main.rand.Next(-30, 31) * 0.2f, Main.rand.Next(-30, 31) * 0.2f) * .4f;
		public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
			{
				var entitySource = NPC.GetSource_Death();
				for(int i = 0; i < 3; i++)
				{
					Gore gore = Gore.NewGoreDirect(entitySource, NPC.Center, RandomVel, Mod.Find<ModGore>("DessgeegaGore"+i).Type, NPC.scale);
					gore.position -= new Vector2(Terraria.GameContent.TextureAssets.Gore[gore.type].Value.Width,Terraria.GameContent.TextureAssets.Gore[gore.type].Value.Height) / 2;
					gore.timeLeft = 60;
				}
				for (int i = 0; i < 15; i++)
				{
					Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 6, RandomVel.X, RandomVel.Y, 100, default(Color), 1.5f*NPC.scale);
					dust.noGravity = false;
				}
			}
		}
	}
	public class Dessgeega_Large : Dessgeega
	{
		private bool spawn = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Large Dessgeega");
			Main.npcFrameCount[NPC.type] = 3;
			NPCID.Sets.MPAllowedEnemies[Type] = true;

			NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
			{
				SpecificallyImmuneTo = new int[] {
					BuffID.OnFire,
					BuffID.CursedInferno
				}
			};
			NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if(Main.hardMode)
			{
				return (spawnInfo.SpawnTileY > WorldGen.lavaLine ? SpawnCondition.Cavern.Chance * 0.05f : 0) + SpawnCondition.Underworld.Chance * 0.1f;
			}
			return 0f;
		}
		
		public override void SetDefaults()
		{
			NPC.width = 96;
			NPC.height = 76;
			NPC.aiStyle = -1;
			NPC.damage = 60;
			NPC.defense = 40;
			NPC.lifeMax = 600;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = Item.buyPrice(0, 0, 12, 0);
			NPC.knockBackResist = 0.1f;
			//banner = NPC.type;
			//bannerItem = mod.ItemType("DessgeegaLargeBanner");
			NPC.noGravity = true;
			NPC.behindTiles = true;
			NPC.lavaImmune = true;
		}
		
		public override bool PreAI()
		{
			if (!spawn)
			{
				spawn = true;
				NPC.netUpdate = true;
			}
			return true;
		}
		
		Vector2 RandomVel => new Vector2(Main.rand.Next(-30, 31) * 0.2f, Main.rand.Next(-30, 31) * 0.2f) * .4f;
		public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
			{
				var entitySource = NPC.GetSource_Death();
				for (int i = 0; i < 3; i++)
				{
					Gore gore = Gore.NewGoreDirect(entitySource, NPC.Center, RandomVel, Mod.Find<ModGore>("DessgeegaLargeGore"+i).Type, NPC.scale);
					gore.position -= new Vector2(Terraria.GameContent.TextureAssets.Gore[gore.type].Value.Width,Terraria.GameContent.TextureAssets.Gore[gore.type].Value.Height) / 2;
					gore.timeLeft = 60;
				}
				for (int i = 0; i < 15; i++)
				{
					Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 6, RandomVel.X, RandomVel.Y, 100, default(Color), 2f);
					dust.noGravity = false;
				}
			}
		}
	}
}
