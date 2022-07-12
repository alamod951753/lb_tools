using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolsDev.Repositories.Dto.CA.ColaTxAuthHist;

namespace ToolsDev.Repository.Interface.CA
{
    public interface IRepositoryColaTxAuthToday
    {
        bool UpdateDESCR(string descr, string seq);
    }
}
