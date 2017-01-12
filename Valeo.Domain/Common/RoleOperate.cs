using System;
using System.Collections.Generic;
using System.Linq;
using Valeo.Domain.Enum;

namespace Valeo.Domain.Common
{
    public static class RoleOperate
    {
        public static bool IsOperateType(dynamic data, EnumOperateType operateType)
        {
            try
            {
                var roleInfo = (List<UserAuthVM>) data;
                var isrole = roleInfo.FirstOrDefault(o => o.Opr_code == (int) operateType);
                return isrole != null;
            }
            catch
            {

                return false;
            }
         
        }

        public static string GetIconic(this int iconic)
        {
            var strIocnic = "";
            switch (iconic)
            {
                case 1:
                    strIocnic = "icon_vipuser";
                    break;
                case 2:
                    strIocnic = "icon_search";
                    break;
                case 3:
                    strIocnic = "icon_orderm";
                    break;
                case 4:
                    strIocnic = "icon_taskm";
                    break;
                case 5:
                    strIocnic = "icon_product";
                    break;
                case 6:
                    strIocnic = "icon_reportm";
                    break;
                case 7:
                    strIocnic = "icon_financialm";
                    break;
                case 8:
                    strIocnic = "icon_userinfo";
                    break;
                case 9:
                    strIocnic = "icon_collectdata";
                    break;
                case 10:
                    strIocnic = "icon_onlinedata";
                    break;
                case 11:
                    strIocnic = "icon_emailm";
                    break;
                case 12:
                    strIocnic = "icon_parameterm";
                    break;
                case 13:
                    strIocnic = "icon_webedite";
                    break;
                case 14:
                    strIocnic = "icon_messagem";
                    break;
                case 15:
                    strIocnic = "icon_cw";
                    break;
                default:
                    strIocnic = "icon-man";
                    break;
            }
            return strIocnic;
        }
    }
}