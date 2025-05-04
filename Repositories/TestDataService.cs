using Homework.Data; 
using Homework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient; 
using System.Data;
using System.Data.SqlClient; 
using Dapper; 

namespace Homework_SkillTree.Services
{
    public class TestDataService
    {
        private readonly MyBlogContext _context;
        private readonly string _connectionString;

        public TestDataService(MyBlogContext context, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _connectionString = configuration.GetConnectionString("SkillTreeDatabase");
        }

        // 使用 Entity Framework 新增資料
        public void AddWithEntityFramework(TestData model)
        {
            _context.AccountBooks.Add(model);
            _context.SaveChanges();
        }

        // 使用 Dapper 新增資料
        public void AddWithDapper(TestData model)
        {
            using (var connection = CreateConnection())
            {
                var sql = @"
                        INSERT INTO AccountBook (Id, Categoryyy, Amounttt, Dateee, Remarkkk)
                        VALUES (@Id, @Category, @Money, @Date, @Description)";
                connection.Execute(sql, model);
            }
        }

        // 建立 Dapper 的資料庫連線
        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
