using AutoMapper;
using LibraryService.Services.Interfaces;
using LibraryUI.Infrastructure;
using LibraryUI.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LibraryUI.Controllers
{
    [MyAuthorizeAttribute]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IHistoryService _historyService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService,
            IHistoryService historyService,
            IMapper mapper)
        {
            _bookService = bookService;
            _historyService = historyService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var items = _bookService.Get();
            var viewModel = new BooksViewModel
            {
                Books = _mapper.Map<List<BookViewModel>>(items)
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Give(int bookId)
        {
            var item = _bookService.Get(bookId);
            var viewModel = _mapper.Map<BookViewModel>(item);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Give(BookViewModel item)
        {
            _bookService.Give(item.Id, CurrentUser.Id);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult BookHistory(int bookId)
        {
            var item = _bookService.Get(bookId);
            ViewBag.Book = item.Name;
            var history = _historyService.GetBookHistory(bookId);
            var viewModel = new HistoriesViewModel
            {
                Histories = _mapper.Map<List<HistoryViewModel>>(history)
            };
            return View(viewModel);
        }
    }
}