using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.UserGrade
{
    [Serializable]
    [PetaPoco.TableName("m_UserDataRelation")]
    [PetaPoco.PrimaryKey("RelationID")]
    public class UserDataRelationModel
    {
        /// <summary>
        /// 主建
        /// </summary>
        public long RelationID { get; set; }

        /// <summary>
        /// 用户级别
        /// </summary>
        public long UserGradeID { get; set; }

        /// <summary>
        /// 数据级别
        /// </summary>
        public long DataGradeID { get; set; }
    }


}
