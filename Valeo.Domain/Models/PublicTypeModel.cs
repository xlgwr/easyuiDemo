using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace Valeo.Domain.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PublicTypeModel
    {
        [ResultColumn]
        public string LanguageName { get; set; }
             
    }
}
