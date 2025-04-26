using System.Collections.Generic;
using System.Diagnostics;
using Homework.Models;
using Homework_SkillTree.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework_SkillTree.Controllers
{
    public class HomeController : Controller
    {
       
        
        private static List<TestData> _transactions = new List<TestData>();
        private static int _currentId = 1; // 流水號起始值

       
        [HttpGet]
        public IActionResult Index()
        {
            // 產生測試資料
            var testData = GenerateTestData();
            // 如果 _transactions 為空，才產生測試資料
            if (!_transactions.Any())
            {
                _transactions.AddRange(testData);
                _currentId = _transactions.Max(t => t.Id) + 1;
            }

            // 將資料傳遞到 View
            return View(_transactions); // 傳遞資料到 index.cshtml
        }



        [HttpPost]
        public IActionResult AddData(TestData model)
        {
            if (ModelState.IsValid)
            {
                model.Id = _currentId++; // 設定流水號
                _transactions.Add(model); // 新增資料到列表
                return RedirectToAction("Index"); // 重新導向到 Index 頁面
            }

            // 如果驗證失敗，返回 Index 頁面，並顯示目前的資料
            return View("Index", _transactions);
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
                    Id = i + 1, // 流水號
                    Category = i % 2 == 0 ? "收入" : "支出", // 偶數為收入，奇數為支出
                    Money = new Random().Next(1, 10000), // 隨機金額，範圍 1 ~ 10000
                    Date = DateTime.Now.AddDays(-i), // 日期為今天往前推 i 天
                    Description = $"這是第 {i + 1} 筆測試資料"
                });
            }
            return data;
        }
    }
}
