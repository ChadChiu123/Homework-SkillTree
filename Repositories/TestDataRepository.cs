using Microsoft.Data.SqlClient; // Updated namespace

using Homework.Models;
using System.Data;
using Dapper;

namespace Homework_SkillTree.Repositories
{
    public class TestDataRepository
    {
        private readonly string _connectionString;

        public TestDataRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SkillTreeDatabase");
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public void AddTestData(TestData model)
        {
            using (var connection = CreateConnection())
            {
                var sql = @"
                    INSERT INTO AccountBook (Id, Categoryyy, Amounttt, Dateee, Remarkkk)
                    VALUES (@Id, @Category, @Money, @Date, @Description)";
                connection.Execute(sql, model);
            }
        }
    }
}
