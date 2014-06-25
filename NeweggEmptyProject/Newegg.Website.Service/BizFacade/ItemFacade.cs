/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Ben Guan (Ben.B.Guan@newegg.com)
 * Create Date:  06/26/2013
 * Usage:
 *
 * RevisionHistory
 * Date         Author               PageDescription
 * 
*****************************************************************/

using Newegg.Website.DomainModel;
using Newegg.Website.Service.BLL;

namespace Newegg.Website.Service.BizFacade
{
    public class ItemFacade
    {
        public static tblItem GetItemInfo(string itemNumber)
        {
            return ItemBLL.GetItemInfo(itemNumber);
        }
        
    }
}
