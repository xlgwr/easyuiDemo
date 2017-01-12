using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Valeo
{
    public abstract class vBase
    {
        [StringLength(255)]
        public string remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(64)]
        public string adduser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(64)]
        public string upduser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime addtime { get; set; }
        public DateTime? updtime { get; set; }
    }
}
