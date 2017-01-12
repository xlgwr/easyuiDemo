namespace Valeo.Domain.SearchDialog
{
    public class PersonSearchVm
    {
        public string FullNameEn { get; set; }

        public string FullNameCn { get; set; }
        /// <summary>
        /// 标记1:法庭 2:公司 3:土地 4:信贷 5:公共
        /// </summary>
        public int Flag { get; set; }
        /// <summary>
        /// 参数(当flag=1时 这里将保存0:原告,1:被告 2:法官 3:原告律师 4:被告律师等)
        /// </summary>
        public int Param1 { get; set; }
    }
}