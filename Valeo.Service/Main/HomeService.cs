using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Valeo.Domain.Models;

namespace Valeo.Service.Main
{
    public class HomeService : BaseService
    {
        public List<SysModuleModel> GetUserSysModule(string userGradeId)
        {
            var topMenus = db.Query<SysModuleModel>("SELECT * from m_SysModule  WHERE Parentid=@0", "0").ToList();

             var nextMenu=db.Query<SysModuleModel>(
                    @"SELECT * from m_SysModule  WHERE Mod_id in(SELECT Mod_id from m_UserAuthority where UserGradeID=@0 and Opr_code=0)",
                    userGradeId).ToList();
            var topToNextMenu =nextMenu.GroupBy(o => o.Parentid).Select(item => topMenus.FirstOrDefault(o => o.Mod_id == item.Key));
            nextMenu.AddRange(topToNextMenu.Where(topMenu => topMenu != null));
            return nextMenu;
        }
    }
}
