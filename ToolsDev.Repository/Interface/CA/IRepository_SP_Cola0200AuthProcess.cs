using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolsDev.Repositories.Dto.CA.ColaTxAuthHist;

namespace ToolsDev.Repository.Interface.CA
{
    public interface IRepository_SP_Cola0200AuthProcess
    {
        string ExecSP_Cola0200_AuthProcess(string cardNbr, string expireDte);
    }
}
