using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolsDev.Repository.Dto.AuthSimulatorApp;

namespace ToolsDev.Repository.Interface.AuthSimulatorApp
{
    public interface IRepositoryRealtimeAuth
    {
        int InsertApi(RealtimeAuthApi_Dto api);
        bool InsertApiList(List<RealtimeAuthApi_Dto> apis);
        int InsertBatch(RealtimeAuthBatch_Dto batch);
        List<RealtimeAuthApi_Dto> GetApiList(RequestGetApiList_Dto request);
        List<ResponseGetBatchList_Dto> GetBatchList(RequestGetBatchList_Dto request);
        List<RealtimeAuthApi_Dto> GetBatchFailDetails(int batchSeq);
        List<RealtimeAuthBatch_Dto> GetBatchToBeProcessedList();
        int UpdateBatchProcessingStart(RequestUpdateBatchProcessingStart_Dto request);
        int UpdateBatchProcessEnd(RequestUpdateBatchProcessEnd_Dto request);
    }
}
