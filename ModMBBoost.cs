﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MetroidModPorted.ID;

namespace MetroidModPorted
{
	public abstract class ModMBBoost : ModMBAddon
	{
		internal override sealed void InternalStaticDefaults()
		{
			AddonSlot = MorphBallAddonSlotID.Boost;
		}
	}
}