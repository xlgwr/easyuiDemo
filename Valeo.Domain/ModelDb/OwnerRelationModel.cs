using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.ModelDb
{
    [Serializable]
    [PetaPoco.TableName("t_OwnerRelation")]
    [PetaPoco.PrimaryKey("OwnerRelationID", autoIncrement = true)]
    public class OwnerRelationModel
    {
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long OwnerRelationID { get; set; }

        /// <summary>
        /// LandID
        /// </summary>
        public virtual long LandID { get; set; }

        /// <summary>
        /// Entityid
        /// </summary>
        public virtual long Entityid { get; set; }
    }
}
