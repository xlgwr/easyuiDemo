using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    /// <summary>
    /// 自动任务组别
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("t_TaskAutoGroup")]
    [PetaPoco.PrimaryKey("TaskGroupID")]
    public class TaskAutoGroupModel
    {
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public long TaskGroupID { get; set; }

        /// <summary>
        /// 任务id
        /// </summary>
        public long TaskID { get; set; }

        /// <summary>
        /// 组名(一个产品只能有5个组,5个组的人总共不能超过购买产品的总人数)
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 查册者
        /// </summary>
        public string SelectName { get; set; }

        /// <summary>
        /// 最后搜索日期时间
        /// </summary>
        public DateTime Latest_date { get; set; }

        /// <summary>
        /// 搜索日期
        /// </summary>
        public DateTime Search_date { get; set; }
         
        /// <summary>
        /// 0:CS上传 1:web上传
        /// </summary>
        public int Type { get; set; }

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