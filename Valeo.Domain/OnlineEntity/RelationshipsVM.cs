using Valeo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.OnlineEntity
{
    public class RelationshipsVM : RelationshipsModel
    {
        public virtual string FullName_En { get; set; }

        public virtual string FullName_Cn { get; set; }

        public virtual string ComboxListName { get; set; }
        public virtual long Type { get; set; }
    }
}
