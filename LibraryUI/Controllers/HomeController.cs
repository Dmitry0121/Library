using AutoMapper;
using LibraryService.Services.Interfaces;
using LibraryUI.Infrastructure;
using LibraryUI.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LibraryUI.Controllers
{
    [MyAuthorizeAttribute]
    public class HomeController : Controller
    {
        private readonly IHistoryService _historyService;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public HomeController(IHistoryService historyService,
            IBookService bookService, 
            IMapper mapper)
        {
            _historyService = historyService;
            _bookService = bookService;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            var history = _historyService.GetUserActiveHistory(CurrentUser.Id);
            var viewModel = new HistoriesViewModel
            {
                Histories = _mapper.Map<List<HistoryViewModel>>(history)
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Return(int historyId)
        {
            var history = _historyService.Get(historyId);
            var viewModel = _mapper.Map<HistoryViewModel>(history);         
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Return(HistoryViewModel item)
        {
            _bookService.Return(item.BookId, item.Id);
            return RedirectToAction("Index");
        }
    }
}