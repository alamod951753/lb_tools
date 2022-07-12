using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolsDev.Service.Entity;
using ToolsDev.Service.Entity.RealtimeService;

namespace ToolsDev.Service.Interface
{
    public interface IRealtimeService
    {
        Task<ResultEntity> InsertApi(RealtimeAuthApiEntity entity);
        Task<ResultEntity> InsertApiList(RealtimeAuthApiListEntity entity);
        Task<ResultEntity> InsertBatchApi(RealtimeAuthBatchEntity entity);
        ResultEntity GetApiList(RequestGetApiListEntity entity);
        ResultEntity GetBatchList(RequestGetBatchListEntity entity);
        ResultEntity GetBatchFailDetails(RequestGetBatchFailDetailsEntity entity);
        ResultEntity GetBatchToBeProcessedList();
        Task<ResultEntity> UpdateBatchProcessingStart(RequestUpdateBatchProcessingStartEntity entity);
        Task<ResultEntity> UpdateBatchProcessEnd(RequestUpdateBatchProcessEndEntity entity);
    }
}
