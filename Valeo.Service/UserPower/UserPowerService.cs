using Valeo.Domain;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Service
{
    public class UserPowerService : BaseService
    {
        /// <summary>
        /// 获取当前用户级别的所有权限
        /// </summary>
        /// <param name="UserGradeID"></param>
        /// <returns></returns>
        public List<SysModuleVM> GetUserPowerList(string UserGradeID)
        {
            //用户权限
            List<SysModuleVM> m_sysModuleVMList = new List<SysModuleVM>();

            //界面权限
            SysModuleVM m_sysModuleVM = new SysModuleVM();

            //所有界面
            List<SysModule> modelList = new List<SysModule>();

            //界面对应权限
            List<SysModuleDetail> DetailList = new List<SysModuleDetail>();

            //用户级别对应的功能
            List<UserAuthority> userAuthority = new List<UserAuthority>();

            Sql sql = new Sql();
            try
            {
                sql.Append(@"select Mod_id,Mod_nm,Status from m_SysModule where 1=1 AND Parentid<>'0' order by sort ");
                //查询出所有界面
                modelList = db.Query<SysModule>(sql).ToList();

                foreach (var ModuleItem in modelList)
                {
                    //根据当前界面id，查出所对应的权限信息
                    sql = new Sql();
                    sql.Append(@"select Mod_id,Opr_code,case Opr_code WHEN '0' THEN'查询' 
							 WHEN '1' THEN '新增' 
							 WHEN '2' THEN '修改' 
							 WHEN '3' THEN '删除' 
							 WHEN '4' THEN '查询' 
							 WHEN '5' THEN '审核' 
							 WHEN '6' THEN '导入' 
							 WHEN '7' THEN '导出'  END as Code_Name from m_SysModuleDetail where 1=1 and Mod_id=@0", ModuleItem.Mod_id);
                    DetailList = new List<SysModuleDetail>();
                    DetailList = db.Query<SysModuleDetail>(sql).ToList();

                    //根据用户级别，查询出已设置过的权限信息
                    sql = new Sql();
                    sql.Append(@"select * from m_UserAuthority where UserGradeID=@0 and Mod_id=@1 ", UserGradeID, ModuleItem.Mod_id);

                    userAuthority = new List<UserAuthority>();
                    userAuthority = db.Query<UserAuthority>(sql).ToList();

                    //组合当前用户级别在所有界面的功能以及设置
                    m_sysModuleVM = new SysModuleVM();
                    m_sysModuleVM.Mod_id = ModuleItem.Mod_id;
                    m_sysModuleVM.Mod_nm = ModuleItem.Mod_nm;
                    m_sysModuleVM.Status = ModuleItem.Status;
                    m_sysModuleVM.Search = 2;
                    m_sysModuleVM.Create = 2;
                    m_sysModuleVM.Edit = 2;
                    m_sysModuleVM.Delete = 2;
                    m_sysModuleVM.Check = 2;
                    m_sysModuleVM.Import = 2;
                    m_sysModuleVM.Export = 2;

                    //循环当前可用权限
                    foreach (var detail in DetailList)
                    {
                        switch (detail.Opr_code)
                        {
                            case 0://如果权限=查询，查看当前用户级别中是否有此权限
                                m_sysModuleVM.Search = 0;
                                foreach (var Item in userAuthority)
                                {
                                    if (detail.Opr_code == Item.Opr_code)
                                    {
                                        m_sysModuleVM.Search = 1;
                                        break;
                                    }
                                }
                                break;
                            case 1:
                                m_sysModuleVM.Create = 0;
                                foreach (var Item in userAuthority)
                                {
                                    if (detail.Opr_code == Item.Opr_code)
                                    {
                                        m_sysModuleVM.Create = 1;
                                        break;
                                    }
                                }
                                break;
                            case 2:
                                m_sysModuleVM.Edit = 0;
                                foreach (var Item in userAuthority)
                                {
                                    if (detail.Opr_code == Item.Opr_code)
                                    {
                                        m_sysModuleVM.Edit = 1;
                                        break;
                                    }
                                }
                                break;
                            case 3:
                                m_sysModuleVM.Delete = 0;
                                foreach (var Item in userAuthority)
                                {
                                    if (detail.Opr_code == Item.Opr_code)
                                    {
                                        m_sysModuleVM.Delete = 1;
                                        break;
                                    }
                                }
                                break;
                            case 4:
                                m_sysModuleVM.Check = 0;
                                foreach (var Item in userAuthority)
                                {
                                    if (detail.Opr_code == Item.Opr_code)
                                    {
                                        m_sysModuleVM.Check = 1;
                                        break;
                                    }
                                }
                                break;
                            case 5:
                                m_sysModuleVM.Import = 0;
                                foreach (var Item in userAuthority)
                                {
                                    if (detail.Opr_code == Item.Opr_code)
                                    {
                                        m_sysModuleVM.Import = 1;
                                        break;
                                    }
                                }
                                break;
                            case 6:
                                m_sysModuleVM.Export = 0;
                                foreach (var Item in userAuthority)
                                {
                                    if (detail.Opr_code == Item.Opr_code)
                                    {
                                        m_sysModuleVM.Export = 1;
                                        break;
                                    }
                                }
                                break;
                            default:
                                break;
                        }

                    }
                    m_sysModuleVMList.Add(m_sysModuleVM);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return m_sysModuleVMList;
        }

        /// <summary>
        /// 保存当前用户级别的权限
        /// </summary>
        /// <param name="UserGradeID"></param>
        /// <param name="model"></param>
        public void EditSave(string UserGradeID, List<SysModuleVM> UserAuthority)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    //删除当前用户级别的权限
                    Sql sqldel = new Sql();
                    sqldel.Append(@"delete from m_UserAuthority where UserGradeID=@0", UserGradeID);
                    int result = db.Execute(sqldel);
                    string str_sql = string.Empty;

                    //添加新的权限信息到表中
                    foreach (var item in UserAuthority)
                    {
                        str_sql = string.Empty;

                        if (item.Search != 0 && item.Search != 2)
                        {
                            str_sql += string.Format(@"insert into m_UserAuthority ( `UserGradeID`, `Mod_id`, `Opr_code`) values('{0}','{1}','{2}');", UserGradeID, item.Mod_id, 0);
                        }
                        if (item.Create != 0 && item.Create != 2)
                        {
                            str_sql += string.Format(@"insert into m_UserAuthority ( `UserGradeID`, `Mod_id`, `Opr_code`) values('{0}','{1}','{2}');", UserGradeID, item.Mod_id, 1);
                        }
                        if (item.Edit != 0 && item.Edit != 2)
                        {
                            str_sql += string.Format(@"insert into m_UserAuthority ( `UserGradeID`, `Mod_id`, `Opr_code`) values('{0}','{1}','{2}');", UserGradeID, item.Mod_id, 2);
                        }
                        if (item.Delete != 0 && item.Delete != 2)
                        {
                            str_sql += string.Format(@"insert into m_UserAuthority ( `UserGradeID`, `Mod_id`, `Opr_code`) values('{0}','{1}','{2}');", UserGradeID, item.Mod_id, 3);
                        }
                        if (item.Check != 0 && item.Check != 2)
                        {
                            str_sql += string.Format(@"insert into m_UserAuthority ( `UserGradeID`, `Mod_id`, `Opr_code`) values('{0}','{1}','{2}');", UserGradeID, item.Mod_id, 4);
                        }
                        if (item.Import != 0 && item.Import != 2)
                        {
                            str_sql += string.Format(@"insert into m_UserAuthority ( `UserGradeID`, `Mod_id`, `Opr_code`) values('{0}','{1}','{2}');", UserGradeID, item.Mod_id, 5);
                        }
                        if (item.Export != 0 && item.Export != 2)
                        {
                            str_sql += string.Format(@"insert into m_UserAuthority ( `UserGradeID`, `Mod_id`, `Opr_code`) values('{0}','{1}','{2}');", UserGradeID, item.Mod_id, 6);
                        }
                        if (str_sql != "")
                        {
                            result = db.Execute(str_sql);
                        }
                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
