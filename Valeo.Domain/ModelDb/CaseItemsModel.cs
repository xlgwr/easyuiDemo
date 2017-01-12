using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("m_Case_Items")]
    [PetaPoco.PrimaryKey("Tid", autoIncrement = true)]
    public class CaseItemsModel
    {
        public long  Tid { get; set; }

        public long Language { get; set; }

       public  string TkeyNo { get; set; }

        public long Tindex { get; set; }

        public long HtmlId { get;set; }

        public int SerialNo { get; set; }

        public  string CourtId { get; set; }
        public string Judge { get; set; }
        public string Year { get; set; }
        public string CourtDay { get; set; }
        public string Hearing { get; set; }
        public string CaseNo { get; set; }
        public string CaseTypeId { get; set; }
        public string Parties { get; set; }
        public string PlainTiff { get; set; }
        public string P_Address { get; set; }
        public string Defendant { get; set; }
        public string D_Address { get; set; }
        public string Nature { get; set; }
        public string Representation { get; set; }
        public string Representation_P { get; set; }
        public string Representation_D { get; set; }
        public string Currency { get; set; }
        public string Amount { get; set; }
        public string Tname { get; set; }
        public string Ttype { get; set; }
        public string Other { get; set; }
        public string Other1 { get; set; }
        public string Remark { get; set; }
        public int Tstatus { get; set; }
        public int Enable { get; set; }
        public string ClientIp { get; set; }
        public string Adduser { get; set; }
        public string Upduser { get; set; }
        public DateTime Addtime { get; set; }
        public DateTime Updtime { get; set; }

    }
}
