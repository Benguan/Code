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

using System.Collections.Generic;
using Newegg.Website.DomainModel;
using Newegg.Framework.DataAccess;

namespace Newegg.Website.Service.DAL
{
    public class ItemDA
    {
        public static tblItem GetItemInfo(string itemNumber)
        {
            string commandName = "GetItemInfo";

            DataCommand command = DataCommandManager.GetDataCommand(commandName);
            command.SetParameterValue("@ItemNumber", itemNumber);

            return command.ExecuteEntity<tblItem>();
        }
    }
}
