
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("t_SocialNetwork")]
    [PetaPoco.PrimaryKey("SocialNetworkID", autoIncrement = true)]
    public class SocialNetworkModel
    {
        #region 实体属性

        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long SocialNetworkID { get; set; }


        /// <summary>
        /// Entityid
        /// </summary>
        public virtual long Entityid { get; set; }

        /// <summary>
        /// 网站名称
        /// </summary>
        public virtual string NetName { get; set; }

        /// <summary>
        /// 网址
        /// </summary>
        public virtual string URL { get; set; }

        /// <summary>
        /// 社交帐号
        /// </summary>
        public virtual string SocialNumber { get; set; }

        private string _SocialDate = "";
        /// <summary>
        /// 社交日期
        /// </summary>
        public virtual string SocialDate
        {
            get
            {

                try
                {
                    var tmpdate = DateTime.Today;
                    if (DateTime.TryParse(_SocialDate, out tmpdate))
                    {
                        _SocialDate = tmpdate.ToString("yyyy-MM-dd");
                    }
                    return _SocialDate;
                }
                catch (Exception)
                {

                    return _SocialDate;
                }


            }
            set
            {
                _SocialDate = value;
            }
        }


        /// <summary>
        /// 照片路径
        /// </summary>
        public virtual string Pic { get; set; }





        #endregion



    }
}
