using System.ComponentModel.DataAnnotations;

namespace Homework.Models
{
    /// <summary>
    /// 建立測試資料的類別
    /// </summary>
    public class TestData
    {
        public int Id { get; set; } // 流水號
        /// <summary>
        /// 類別 (支出/收入)
        /// </summary>
        [Required]
        public string Category { get; set; }
        /// <summary>
        /// 金額
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "金額必須大於 0")]
        public int Money { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string Description { get; set; }
    }
}
