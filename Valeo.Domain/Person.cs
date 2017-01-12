using System;

namespace Valeo.Domain
{
    /// <summary>
    /// 人员信息实体
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("PersonInfo")]
    [PetaPoco.PrimaryKey("PersonID")]
    public class Person
    {
        /// <summary>
        /// 人员编号
        /// </summary>
        public string PersonID { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string PersonName { get; set; }

        /// <summary>
        /// 人员年龄
        /// </summary>
        public int PersonAge { get; set; }
    }
}
