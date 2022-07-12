using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Repositories.Dto.CARD_INF
{
    /// <summary>
    /// CARD_INF Layout
    /// </summary>
    public class CardInf_Dto
    {
        public string ACCT_NBR { get; set; }
        public string CARD_NBR { get; set; }
        public string LINK_BANK_NBR { get; set; }
        public string EXPIRE_DTE { get; set; }
        public string EXPIRE_DTE_LAST { get; set; }
    }
}
