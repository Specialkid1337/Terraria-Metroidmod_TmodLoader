using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace MetroidMod.Projectiles.plasmabeamgreen
{
	public class PlasmaBeamGreenShot : MProjectile
	{
		public override void SetDefaults()
		{
			base.SetDefaults();
			projectile.name = "Plasma Beam Green Shot";
			projectile.width = 8;
			projectile.height = 8;
			projectile.scale = 2f;
			projectile.penetrate = 6;
			projectile.usesLocalNPCImmunity = true;
       	 	projectile.localNPCHitCooldown = 10;
		}

		int dustType = 61;
		Color color = MetroidMod.plaGreenColor;
		public override void AI()
		{
			if(projectile.name.Contains("Ice"))
			{
				dustType = 59;
				color = MetroidMod.iceColor;
			}
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			Lighting.AddLight(projectile.Center, color.R/255f,color.G/255f,color.B/255f);
			
			if(projectile.name.Contains("Spazer") || projectile.name.Contains("Wave"))
			{
				mProjectile.WaveBehavior(projectile, !projectile.name.Contains("Wave"));
			}
			
			if(projectile.numUpdates == 0)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0, 0, 100, default(Color), projectile.scale);
				Main.dust[dust].noGravity = true;
			}
		}
		public override void Kill(int timeLeft)
		{
			mProjectile.DustyDeath(projectile, dustType);
		}
		
		public override bool PreDraw(SpriteBatch sb, Color lightColor)
		{
			mProjectile.PlasmaDraw(projectile, Main.player[projectile.owner], sb);
			return false;
		}
	}
	
	public class SpazerPlasmaBeamGreenShot : PlasmaBeamGreenShot
	{
		public override void SetDefaults()
		{
			base.SetDefaults();
			projectile.name = "Spazer Plasma Beam Green Shot";
			
			mProjectile.amplitude = 7.5f*projectile.scale;
			mProjectile.wavesPerSecond = 2f;
			mProjectile.delay = 6;
		}
	}
	
	public class WavePlasmaBeamGreenShot : PlasmaBeamGreenShot
	{
		public override void SetDefaults()
		{
			base.SetDefaults();
			projectile.name = "Wave Plasma Beam Green Shot";
			projectile.tileCollide = false;
			
			mProjectile.amplitude = 8f*projectile.scale;
			mProjectile.wavesPerSecond = 1f;
			mProjectile.delay = 6;
		}
	}
	
	public class WaveSpazerPlasmaBeamGreenShot : WavePlasmaBeamGreenShot
	{
		public override void SetDefaults()
		{
			base.SetDefaults();
			projectile.name = "Wave Spazer Plasma Beam Green Shot";
			mProjectile.amplitude = 12f*projectile.scale;
		}
	}
	
	public class IcePlasmaBeamGreenShot : PlasmaBeamGreenShot
	{
		public override void SetDefaults()
		{
			base.SetDefaults();
			projectile.name = "Ice Plasma Beam Green Shot";
		}
	}
	
	public class IceSpazerPlasmaBeamGreenShot : SpazerPlasmaBeamGreenShot
	{
		public override void SetDefaults()
		{
			base.SetDefaults();
			projectile.name = "Ice Spazer Plasma Beam Green Shot";
		}
	}
	
	public class IceWavePlasmaBeamGreenShot : WavePlasmaBeamGreenShot
	{
		public override void SetDefaults()
		{
			base.SetDefaults();
			projectile.name = "Ice Wave Plasma Beam Green Shot";
		}
	}
	
	public class IceWaveSpazerPlasmaBeamGreenShot : WaveSpazerPlasmaBeamGreenShot
	{
		public override void SetDefaults()
		{
			base.SetDefaults();
			projectile.name = "Ice Wave Spazer Plasma Beam Green Shot";
		}
	}
}
