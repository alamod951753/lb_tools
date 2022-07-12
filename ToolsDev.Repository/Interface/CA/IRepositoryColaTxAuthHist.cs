using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolsDev.Repositories.Dto.CA.ColaTxAuthHist;

namespace ToolsDev.Repository.Interface.CA
{
    public interface IRepositoryColaTxAuthHist
    {
        List<string> GetColaTxAuthHistReceiveDte(int selectNum = 14);
        List<Rsp_GetColaTxAuthHistForNCCC_Dto> GetColaTxAuthHistForNCCC(Req_GetColaTxAuthHistForNCCC_Dto request);
    }
}
