﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Valeo
{
    /// <summary>
    /// 客户
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("v_customer")]
    [PetaPoco.PrimaryKey("customerNo", autoIncrement = false)]
    public class v_customer : vBase
    {
        #region Model
        /// <summary>
        /// key
        /// </summary>
        public string customerNo
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string customerName
        {
            set;
            get;
        }
        /// <summary>
        /// 简称
        /// </summary>
        public string abbreviation
        {
            set;
            get;
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string tel
        {
            set;
            get;
        }
        /// <summary>
        /// 传真
        /// </summary>
        public string fax
        {
            set;
            get;
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string contacts
        {
            set;
            get;
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string address
        {
            set;
            get;
        }
        #endregion Model
    }
}
