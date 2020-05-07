using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sweb.ViewModels
{
    public class TeacherViewModel
    {
        public TeacherViewModel() { }

        public int Id { get; set; }

        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$")]
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$")]
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$")]
        [MaxLength(50)]
#nullable enable
        public string? Degree { get; set; }

        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$")]
        [MaxLength(25)]
#nullable enable
        public string? AcademicRank { get; set; }

        [RegularExpression("^[0-9]+$")]
        [MaxLength(10)]
#nullable enable
        public string? OfficeNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? HireDate { get; set; }
#nullable enable
        public string? filePath { get; set; }
    }
}
