using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Valeo
{
    /// <summary>
    /// 车辆信息
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("v_carsupport")]
    [PetaPoco.PrimaryKey("carNo", autoIncrement = false)]
    public class v_carsupport : vBase
    {
        #region Model
        /// <summary>
        /// key
        /// </summary>
        public string carNo
        {
            set;
            get;
        }
        /// <summary>
        /// 车牌号码
        /// </summary>
        public string carNumber
        {
            set;
            get;
        }
        /// <summary>
        /// 司机负责人
        /// </summary>
        public string driver
        {
            set;
            get;
        }
        /// <summary>
        /// 司机电话
        /// </summary>
        public string driverTel
        {
            set;
            get;
        }
        /// <summary>
        /// 车辆状态：
        /// 0：可用
        /// 1：不可用
        /// </summary>
        public int status
        {
            set;
            get;
        }
        #endregion Model
    }
}
