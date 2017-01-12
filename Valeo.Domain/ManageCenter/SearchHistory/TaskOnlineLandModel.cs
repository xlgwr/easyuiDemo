using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    /// <summary>
    /// 在线土地任务表
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("t_TaskOnlineLand")]
    [PetaPoco.PrimaryKey("TaskID")]
    public class TaskOnlineLandModel
    {
        /// <summary>
        /// 任务id (50201605270001)
        /// </summary>
        public string TaskID { get; set; }

        /// <summary>
        /// 订单明细id
        /// </summary>
        public string OrderListID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public string MemberID { get; set; }

        /// <summary>
        /// 进度(0:未处理 1:处理中 2:取消 3:完成)
        /// </summary>
        public string Progress { get; set; }

        /// <summary>
        /// 完成人
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 大厦名称
        /// </summary>
        public string BuildName { get; set; }

        /// <summary>
        /// HouseNO
        /// </summary>
        public string HouseNO { get; set; }

        /// <summary>
        /// 房号(单位号)
        /// </summary>
        public string RoomNO { get; set; }

        /// <summary>
        /// 楼层数
        /// </summary>
        public string Floor { get; set; }

        /// <summary>
        /// 座号
        /// </summary>
        public string SeatNO { get; set; }

        /// <summary>
        /// 街道编号
        /// </summary>
        public string StreetNumber { get; set; }

        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 新界的用到
        /// </summary>
        public string LotType { get; set; }

        /// <summary>
        /// 地段编号
        /// </summary>
        public string LotNo { get; set; }


        /// <summary>
        /// 查册者
        /// </summary>
        public string SelectName { get; set; }

        /// <summary>
        /// 查册者称谓(Mr/Mrs/Ms)
        /// </summary>
        public string SelectSalutation { get; set; }

        /// <summary>
        /// 查册者电话
        /// </summary>
        public string SelectTel { get; set; }

        /// <summary>
        /// 查册者邮箱
        /// </summary>
        public string SelectEmail { get; set; }

        /// <summary>
        /// 备注(会员填的)
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 添加者
        /// </summary>
        public string adduser { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string upduser { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime? addtime { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? updtime { get; set; }

    }
}