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

using System;
using System.Data;
using Newegg.Framework.Entity;

namespace Newegg.Website.DomainModel
{
    [Serializable]
    public  class tblItem
    {
        #region fileds
        private string itemNumber;
        private bool isHot;
        #endregion

        #region properties
        [DataMapping("IsHot", DbType.Boolean)]
        public bool IsHot
        {
            get { return this.isHot; }
            set { this.isHot = value; }
        }

        [DataMapping("ItemNumber", DbType.String)]
        public string ItemNumber
        {
            get { return this.itemNumber; }
            set { this.itemNumber = value; }
        }
        #endregion
    }
}
