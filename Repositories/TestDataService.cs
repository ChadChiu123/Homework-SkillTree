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

        // �ϥ� Entity Framework �s�W���
        public void AddWithEntityFramework(TestData model)
        {
            _context.AccountBooks.Add(model);
            _context.SaveChanges();
        }

        // �ϥ� Dapper �s�W���
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

        // �إ� Dapper ����Ʈw�s�u
        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
