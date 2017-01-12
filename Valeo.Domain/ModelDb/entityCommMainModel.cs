using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("entityCommMain")]
    [PetaPoco.PrimaryKey("Tid",autoIncrement=true)]
    public partial class EntityCommMainModel
    {
         public  long Tid { get; set; }
         public long Language { get; set; }
         public string tname { get; set; }
         public string ttype { get; set; }
         public string thtml { get; set; }
         public string Remark { get; set; }
         public int tStatus { get; set; }
         public string ClientIP { get; set; }
         public string adduser { get; set; }
         public string upduser { get; set; }
         public DateTime? addtime { get; set; }
         public DateTime? updtime { get; set; }

    }
}
