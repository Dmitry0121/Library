using LibraryService.Dto;
using System.Collections.Generic;

namespace LibraryService.Services.Interfaces
{
    public interface IHistoryService : IServiceRead<HistoryDto>
    {
        List<HistoryDto> GetUserHistory(int id);
        List<HistoryDto> GetUserActiveHistory(int id);
        List<HistoryDto> GetBookHistory(int id);
    }
}