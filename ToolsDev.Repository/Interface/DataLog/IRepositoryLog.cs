using System.Collections.Generic;
using ToolsDev.Repositories.Dto.Log;

namespace ToolsDev.Repository.Interface.CA
{
    public interface IRepositoryLog
    {
        bool InsertList(List<Log_Dto> logs);
    }
}
