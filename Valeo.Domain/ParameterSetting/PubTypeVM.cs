using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.ParameterSetting
{
    public class PubTypeVM
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual long PublicTypeID { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual string TypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string LanguageCode { get; set; }

        public virtual bool isCheck{ get; set; }

        public long TableId { get; set; }
        public long [] TableIdS { get; set; }
    }
}
