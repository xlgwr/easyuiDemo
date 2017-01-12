
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("t_Address")]
    [PetaPoco.PrimaryKey("AddressID",autoIncrement=true)]
    public  class AddressModel
    {
        #region 实体属性

        /// <summary>
        /// 
        /// </summary>
        public long AddressID { get; set; }

        /// <summary>
        /// 人或公司编号
        /// </summary>
        public long Entityid { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address{ get; set; }

        /// <summary>
        /// 大厦名称
        /// </summary>
        public string BuildName { get; set; }

        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// 街号牌
        /// </summary>
        public string StreetNumber { get; set; }

        /// <summary>
        /// 座号
        /// </summary>
        public string SeatNO { get; set; }

        /// <summary>
        /// 人或公司编号
        /// </summary>
        public string Floor { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public string RoomNO { get; set; }

        /// <summary>
        /// 屋号
        /// </summary>
        public string HouseNO { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 地段类型
        /// </summary>
        public string LotType { get; set; }

        /// <summary>
        /// 地段编号
        /// </summary>
        public string LotNo { get; set; }

        /// <summary>
        /// 分段
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// 小分段
        /// </summary>
        public string Subsection { get; set; }

        /// <summary>
        /// 邮政信箱
        /// </summary>
        public string POBox { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Resident(住址)
        /// </summary>
        public string AddressType { get; set; }

        /// <summary>
        /// 所有权 1:拥有Owned 2:租用Rented
        /// </summary>
        public long OwnerShip { get; set; }

        /// <summary>
        /// 生活时长(31年00月)
        /// </summary>
        public string LenghtTime { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 