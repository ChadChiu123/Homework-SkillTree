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
        // ���շs�W��� (�ϥ� Entity Framework)
        [HttpPost("AddWithEF")]
        public IActionResult AddTestDataWithEF()
        {
            var account = new TestData
            {
                Id = Guid.NewGuid(),
                Category = 1,
                Money = 500,
                Date = DateTime.Now,
                Description = "���ո�� (EF)"
            };

            _service.AddWithEntityFramework(account);
            return Ok(new { message = "�s�W���\ (EF)", data = account });
        }

        // ���շs�W��� (�ϥ� Dapper)
        [HttpPost("AddWithDapper")]
        public IActionResult AddTestDataWithDapper([FromBody] TestData model)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid(); // �]�w�y����
                _service.AddWithDapper(model); // �ϥ� Dapper �s�W���
                return Ok(new { message = "�s�W���\ (Dapper)", data = model });
            }

            // �p�G���ҥ��ѡA��^���~�T��
            return BadRequest(ModelState);
        }
    }
}
