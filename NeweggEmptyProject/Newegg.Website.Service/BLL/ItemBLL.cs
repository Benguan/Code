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
using Newegg.Website.Service.DAL;

namespace Newegg.Website.Service.BLL
{
    public  class ItemBLL
    {
        public static tblItem GetItemInfo(string itemNumber)
        {
            return ItemDA.GetItemInfo(itemNumber);
        }
    }
}
