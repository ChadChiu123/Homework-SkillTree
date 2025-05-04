using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework.Models
{
    /// <summary>
    /// 建立測試資料的類別
    /// </summary>
    [Table("AccountBook")] // 對應資料表名稱
    public class TestData
    {
        [Key] // 主鍵
        public Guid Id { get; set; }

        [Column("Categoryyy")]
        [Range(0, 1, ErrorMessage = "Category 必須是 0（支出）或 1（收入）。")]
        public required int Category { get; set; } // 1=收入, 0=支出

        [Column("Amounttt")]
        [Range(1, int.MaxValue, ErrorMessage = "金額必須大於 0。")]
        public int Money { get; set; }

        [Column("Dateee")]
        public DateTime Date { get; set; }

        [Column("Remarkkk")]
        [StringLength(500, ErrorMessage = "備註不能超過 500 個字元。")]
        public string Description { get; set; }
    }
}
