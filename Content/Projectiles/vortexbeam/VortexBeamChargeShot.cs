using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MetroidMod.Content.Projectiles.vortexbeam
{
	public class VortexBeamChargeShot : MProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vortex Beam Charge Shot");
		}
		public override void SetDefaults()
		{
			base.SetDefaults();
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.scale = 2f;
			Main.projFrames[Projectile.type] = 2;
			
			mProjectile.amplitude = 10f*Projectile.scale;
			mProjectile.wavesPerSecond = 2f;
			mProjectile.delay = 8;
		}

		int dustType = 229;
		Color color = MetroidMod.lumColor;
		float scale = 1f;
		public override void AI()
		{
			if(Projectile.Name.Contains("Stardust"))
			{
				dustType = 88;
				color = MetroidMod.iceColor;
				scale = 0.5f;
			}
			else if(Projectile.Name.Contains("Nebula"))
			{
				dustType = 255;
				color = MetroidMod.waveColor;
			}
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			Lighting.AddLight(Projectile.Center, color.R/255f,color.G/255f,color.B/255f);
			if(Projectile.numUpdates == 0)
			{
				Projectile.frame++;
			}
			if(Projectile.frame > 1)
			{
				Projectile.frame = 0;
			}
			
			mProjectile.WaveBehavior(Projectile, !Projectile.Name.Contains("Nebula"));
			if(Projectile.Name.Contains("Nebula"))
			{
				mProjectile.HomingBehavior(Projectile);
			}
			
			int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0, 0, 100, default(Color), Projectile.scale*scale);
			Main.dust[dust].noGravity = true;
			if(Projectile.Name.Contains("Stardust"))
			{
				dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemTopaz, 0, 0, 100, default(Color), Projectile.scale);
				Main.dust[dust].noGravity = true;
			}
		}
		public override void Kill(int timeLeft)
		{
			mProjectile.Diffuse(Projectile, dustType);
		}
		
		public override bool PreDraw(ref Color lightColor)
		{
			mProjectile.PlasmaDraw(Projectile,Main.player[Projectile.owner], Main.spriteBatch);
			return false;
		}
	}
	
	public class NebulaVortexBeamChargeShot : VortexBeamChargeShot
	{
		public override void SetDefaults()
		{
			base.SetDefaults();
			Projectile.Name = "Nebula Vortex Beam Charge Shot";
			Projectile.tileCollide = false;
			
			mProjectile.amplitude = 14f*Projectile.scale;
			mProjectile.wavesPerSecond = 1f;
		}
	}
	
	public class StardustVortexBeamChargeShot : VortexBeamChargeShot
	{
		public override void SetDefaults()
		{
			base.SetDefaults();
			Projectile.Name = "Stardust Vortex Beam Charge Shot";
		}
	}
	
	public class StardustNebulaVortexBeamChargeShot : NebulaVortexBeamChargeShot
	{
		public override void SetDefaults()
		{
			base.SetDefaults();
			Projectile.Name = "Stardust Nebula Vortex Beam Charge Shot";
		}
	}
}
