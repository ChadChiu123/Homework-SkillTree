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
            // �p�G��Ʈw���šA���ʹ��ո��
            if (!_context.AccountBooks.Any())
            {
                var testData = GenerateTestData();
                _context.AccountBooks.AddRange(testData);
                _context.SaveChanges();
            }

            // �q��Ʈw���o��ƨ��e�Q���A�̷� Date �̷s���Ƨ�
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

            // �p�G���ҥ��ѡA��^ Index �����A����ܥثe�����
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
                    Id = Guid.NewGuid(), // �y����
                    Category = i % 2 == 1 ? 1 : 0, // 1=���J�A0=��X
                    Money = new Random().Next(1, 10000), // �H�����B�A�d�� 1 ~ 10000
                    Date = DateTime.Now.AddDays(-i), // ��������ѩ��e�� i ��
                    Description = $"�o�O�� {i + 1} �����ո��"
                });
            }
            return data;
        }
    }
}
