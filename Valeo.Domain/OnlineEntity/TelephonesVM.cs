using Valeo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.OnlineEntity
{
    public class TelephonesVM : TelephonesModel
    {
        public virtual string ComboxListName { get; set; }
    }
}
