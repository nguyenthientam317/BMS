using Book_Management_System.Infrastructure;
using Book_Management_System.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Book_Management_System.Controllers
{
    public class HomeController : Controller
    {
        Model Db = new Model();
        // GET: Home
        public ActionResult Index(int? page)
        {
            var ListBook = Db.Books.ToList();
            return View(PaginatedList<Book>.CreateAsync(ListBook, page ?? 1, ConstantDefine.PAGE_SIZE_INDEX)); // page: đang ở trang nào, trang 1 2 ,3,..
        }
        public ActionResult DetailProduct(string idBook)
        {
            var Item = Db.Books.Find(idBook);
            if(Item != null)
            {
                return View(Item);
            }
            else
            {
                return Redirect(ConstantDefine.HOME_URL);
            }
            

        }
        [ChildActionOnly]
        public ActionResult BookCategories()
        {
            var ListCate = Db.BookCategories.ToList();
            return PartialView(ListCate); // tên method phải trùng vs tên View
            //. Nếu không thì phải truyền vào parameter trong PartialView tên của View cần truyền
        }
        [ChildActionOnly]
        public ActionResult Authors()
        {
            var ListAuthors = Db.Authors.ToList();
            return PartialView(ListAuthors);
        }
        public ActionResult Search(string searchString, string sortOrder, string author, string cate, string currentFilter, int? page)
        {
            var ListBookSearch = Db.Books.ToList();
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SortNew"] = ConstantDefine.NEW_SORT;
            ViewData["SortPriceDes"] = ConstantDefine.PRICE_DESC_SORT;
            ViewData["SortPrice"] = ConstantDefine.PRICE_SORT;
            if(searchString == "")
            {
                return Redirect(ConstantDefine.HOME_URL);
            }
            if (searchString !=null)
            {
                ListBookSearch = ListBookSearch.Where(l => l.Title.Contains(searchString)).ToList();
            }
            else if (currentFilter != null)
            {
                searchString = currentFilter;

                if (author == null)
                {
                    ListBookSearch = ListBookSearch.Where(l => l.Title.Contains(currentFilter) && l.IdCategory.Equals(cate)).ToList();
                }
                else if (cate == null)
                {
                    ListBookSearch = ListBookSearch.Where(l => l.Title.Contains(currentFilter) && l.IdAuthor.Equals(author)).ToList();
                }
                else
                {
                    ListBookSearch = ListBookSearch.Where(l => l.Title.Contains(currentFilter) && l.IdAuthor.Equals(author) && l.IdCategory.Equals(cate)).ToList();
                }
            }
            if (currentFilter == null && searchString == null)
            {

                if (author == null)
                {
                    ListBookSearch = ListBookSearch.Where(l => l.IdCategory.Equals(cate)).ToList();
                }
                else if (cate == null)
                {
                    ListBookSearch = ListBookSearch.Where(l => l.IdAuthor.Equals(author)).ToList();
                }
                else
                {
                    ListBookSearch = ListBookSearch.Where(l => l.IdAuthor.Equals(author) && l.IdCategory.Equals(cate)).ToList();
                }

            }
            TempData["CurrentFilter"] = searchString;
            TempData["CurrentCate"] = cate;
            TempData["CurrentAuthor"] = author;
            //TempData["CurrentCate"] = cate;
            switch (sortOrder)
            {
                case "New":
                    ListBookSearch = ListBookSearch.OrderByDescending(l => l.CreateDate).ToList();
                    break;
                case "Price":
                    ListBookSearch = ListBookSearch.OrderBy(l => l.Price).ToList();
                    break;
                case "PriceDesc":
                    ListBookSearch = ListBookSearch.OrderByDescending(l => l.Price).ToList();
                    break;

                default:
                    ListBookSearch = ListBookSearch.OrderByDescending(l => l.CreateDate).ToList();
                    break;
            }
            return View(PaginatedList<Book>.CreateAsync(ListBookSearch, page ?? 1, ConstantDefine.PAGE_SIZE_SEARCHING));
        }

        [ChildActionOnly]
        public ActionResult Comments(string idBook)
        {
            var ListComment = Db.Comments.Where(l => l.IdBook.Equals(idBook));
            return PartialView(ListComment);
        }
        [HttpPost]
        public JsonResult AddComment(string comment)
        {
            var ListCmt = new JavaScriptSerializer().Deserialize<List<Comment>>(comment);
            foreach (var Cmt in ListCmt)
            {
                Cmt.CreateDate = DateTime.Now;
                Cmt.Id = FindNextId();
                Cmt.IsActive = true;
                Db.Comments.Add(Cmt);
                Db.SaveChanges();
            }

            return Json(new
            {
                status = true
            });
        }
        [HttpPost]
        public JsonResult LoadCommentByIdBook(string idBook)
        {
            var Item = Db.Comments.Where(l => l.IdBook.Equals(idBook)).ToList();
            Item = Item.OrderByDescending(l => l.CreateDate).Take(5).ToList();
            //var JsonItem = new JavaScriptSerializer().Serialize(Item);
            var Jsonlist = JsonConvert.SerializeObject(Item,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
            return Json(new
            {
                data = Jsonlist,
                status = true
            });
        }
        public string FindNextId()
        {
            var ListId = Db.Comments.ToList().Select(l => l.Id);
            int i = 1;
            var Date = DateTime.Now;
            string NewId = $"{Date.Month}{Date.Year.ToString().Substring(2)}_{i}";
            while (ListId.Where(p => p.Equals(NewId)).Count() > 0)
            {
                i++;
                NewId = $"{Date.Month}{Date.Year.ToString().Substring(2)}_{i}";
            }
            return NewId;

        }
    }
}