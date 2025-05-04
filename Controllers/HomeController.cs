using Homework.Data;
using Homework.Models;
using Homework_SkillTree.Models;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homework_SkillTree.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyBlogContext _context;

        public HomeController(MyBlogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public IActionResult Index()
        {
            // 如果資料庫為空，產生測試資料
            if (!_context.AccountBooks.Any())
            {
                var testData = GenerateTestData();
                _context.AccountBooks.AddRange(testData);
                _context.SaveChanges();
            }

            // 從資料庫取得資料取前十筆，依照 Date 最新的排序
            var transactions = _context.AccountBooks
               .OrderByDescending(t => t.Date) 
               .Take(10)
               .ToList();
            return View(transactions);
        }

        [HttpPost]
        public IActionResult AddData(TestData model)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                _context.AccountBooks.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // 如果驗證失敗，返回 Index 頁面，並顯示目前的資料
            var transactions = _context.AccountBooks.ToList();
            return View("Index", transactions);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<TestData> GenerateTestData()
        {
            var data = new List<TestData>();
            for (int i = 0; i < 10; i++)
            {
                data.Add(new TestData
                {
                    Id = Guid.NewGuid(), // 流水號
                    Category = i % 2 == 1 ? 1 : 0, // 1=收入，0=支出
                    Money = new Random().Next(1, 10000), // 隨機金額，範圍 1 ~ 10000
                    Date = DateTime.Now.AddDays(-i), // 日期為今天往前推 i 天
                    Description = $"這是第 {i + 1} 筆測試資料"
                });
            }
            return data;
        }
    }
}
