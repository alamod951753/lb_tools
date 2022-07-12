using System.Collections.Generic;
using ToolsDev.Repositories.Dto.CARD_INF;
using ToolsDev.Repositories.Dto.Log;

namespace ToolsDev.Repository.Interface.CARD_INF
{
    public interface IRepositoryCardInf
    {
        List<CardInf_Dto> GetCardInfList(int selectTop = 1000);
    }
}
