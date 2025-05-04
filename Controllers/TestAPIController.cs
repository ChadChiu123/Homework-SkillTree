using Homework.Models;
using Homework_SkillTree.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAPIController : Controller
    {
        private readonly TestDataService _service;

        public TestAPIController(TestDataService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        // 測試新增資料 (使用 Entity Framework)
        [HttpPost("AddWithEF")]
        public IActionResult AddTestDataWithEF()
        {
            var account = new TestData
            {
                Id = Guid.NewGuid(),
                Category = 1,
                Money = 500,
                Date = DateTime.Now,
                Description = "測試資料 (EF)"
            };

            _service.AddWithEntityFramework(account);
            return Ok(new { message = "新增成功 (EF)", data = account });
        }

        // 測試新增資料 (使用 Dapper)
        [HttpPost("AddWithDapper")]
        public IActionResult AddTestDataWithDapper([FromBody] TestData model)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid(); // 設定流水號
                _service.AddWithDapper(model); // 使用 Dapper 新增資料
                return Ok(new { message = "新增成功 (Dapper)", data = model });
            }

            // 如果驗證失敗，返回錯誤訊息
            return BadRequest(ModelState);
        }
    }
}
