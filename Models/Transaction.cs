using System;
using System.ComponentModel.DataAnnotations;

namespace Homework.Models
{
    public class Transaction
    {
        public int Id { get; set; } // �y����
        /// <summary>
        /// ���O (��X/���J)
        /// </summary>
        [Required]
        public string Category { get; set; }
        /// <summary>
        /// ���B
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "���B�����j�� 0")]
        public int Money { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } 
        /// <summary>
        /// �Ƶ�
        /// </summary>
        public string Description { get; set; } 
    }
}
