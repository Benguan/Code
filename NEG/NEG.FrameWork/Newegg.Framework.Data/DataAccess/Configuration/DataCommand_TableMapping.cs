/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Charlie Yuan (Charlie.J.Yuan@newegg.com)
 * Create Date:  2010-3-15 10:20:50
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace Newegg.Framework.DataAccess.Configuration
{
    [XmlRoot("DataCommand_TableMapping", Namespace = "http://www.newegg.com/Website")]
    public class DataCommand_TableMappingConfig
    {
        #region field
        private UnicodeSteps m_UnicodeSteps;
        private ItemList m_ItemList;
        #endregion

        [XmlArray("unicodeSteps")]
        [XmlArrayItem("add")]
        public UnicodeSteps UnicodeSteps
        {
            get
            {
                return m_UnicodeSteps;
            }
            set
            {
                m_UnicodeSteps = value;
            }
        }

        [XmlArray("itemList")]
        [XmlArrayItem("item")]
        public ItemList ItemList
        {
            get
            {
                return m_ItemList;
            }
            set
            {
                m_ItemList = value;
            }
        }
    }

    public class UnicodeSteps : KeyedCollection<string, UnicodeStep>
    {

        public UnicodeSteps()
            : base(StringComparer.InvariantCultureIgnoreCase)
        {
        }

        #region method
        protected override string GetKeyForItem(UnicodeStep item)
        {
            return item.Step;
        }
        #endregion
    }

    public class UnicodeStep
    {
        #region field
        private string m_Step;
        private bool m_Enable;
        #endregion

        [XmlAttribute("step")]
        public string Step
        {
            get
            {
                return m_Step;
            }
            set
            {
                m_Step = value;
            }
        }

        [XmlAttribute("enable")]
        public bool Enable
        {
            get
            {
                return m_Enable;
            }
            set
            {
                m_Enable = value;
            }
        }
    }

    public class ItemList : KeyedCollection<string, Item>
    {
        public ItemList()
            : base(StringComparer.InvariantCultureIgnoreCase)
        {
        }

        #region method
        protected override string GetKeyForItem(Item item)
        {
            return item.Name;
        }
        #endregion
    }

    public class Item
    {
        #region field
        private string m_Name;
        private SO m_SO;
        private Invoice m_Invoice;
        private RMA m_RMA;
        private Customer m_Customer;
        #endregion

        [XmlAttribute("name")]
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        [XmlElement("so")]
        public SO SO
        {
            get
            {
                return m_SO;
            }
            set
            {
                m_SO = value;
            }
        }

        [XmlElement("invoice")]
        public Invoice Invoice
        {
            get
            {
                return m_Invoice;
            }
            set
            {
                m_Invoice = value;
            }
        }

        [XmlElement("rma")]
        public RMA RMA
        {
            get
            {
                return m_RMA;
            }
            set
            {
                m_RMA = value;
            }
        }

        [XmlElement("customer")]
        public Customer Customer
        {
            get
            {
                return m_Customer;
            }
            set
            {
                m_Customer = value;
            }
        }
    }

    public class SO
    {
        #region field
        private string m_Value;
        #endregion

        [XmlAttribute("value")]
        public string Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
            }
        }
    }

    public class Invoice
    {
        #region field
        private string m_Value;
        #endregion

        [XmlAttribute("value")]
        public string Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
            }
        }
    }

    public class RMA
    {
        #region field
        private string m_Value;
        #endregion

        [XmlAttribute("value")]
        public string Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
            }
        }
    }

    public class Customer
    {
        #region field
        private string m_Value;
        #endregion

        [XmlAttribute("value")]
        public string Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
            }
        }
    }
}
