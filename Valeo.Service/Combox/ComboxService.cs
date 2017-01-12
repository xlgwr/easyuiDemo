using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Valeo.Domain;
using PetaPoco;
using Valeo.Domain.Combox;

namespace Valeo.Service
{
    public class ComboxService : BaseService
    {
        public List<ComboxListModel> getList(EnumCombox e)
        {
            var tmpModels = new List<ComboxListModel>();
            try
            {
                tmpModels = db.Fetch<ComboxListModel>("select * from m_ComboxList where ComboxId=@0 order by DspNo", (int)e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tmpModels;
        }
    }
}
