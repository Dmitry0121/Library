using System;
using System.Collections.Generic;

namespace LibraryUI.Models
{
    public class HistoriesViewModel
    {
        public HistoriesViewModel()
        {
            Histories = new List<HistoryViewModel>();
        }

        public List<HistoryViewModel> Histories { get; set; }
    }

    public class HistoryViewModel
    {
        public int Id { get; set; }
        public DateTime DateReceiving { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int BookId { get; set; }
        public BookViewModel Book { get; set; }
        public int UserId { get; set; }
        public UserViewModel User { get; set; }
    }
}