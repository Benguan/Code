/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Jason Huang (jason.j.huang@newegg.com)
 * Create Date:  07/15/2008 10:13:57
 * Usage:
 *
 * RevisionHistory
 * Date         Author               PageDescription
 * 
*****************************************************************/

namespace Newegg.Framework.Utility
{
	public interface IMoneyCalculator
	{
		decimal Calculate(decimal money);
	}
}
