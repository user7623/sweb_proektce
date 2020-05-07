
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SWEB_app.Models;
namespace sweb.ViewModels
{
    public class StudentViewModel
    {
        public StudentViewModel() { }

        public Int64 Id { get; set; }

        [MaxLength(10)]
        [MinLength(4)]
        [RegularExpression("^[0-9]*$")]
        [Required]
        public string Indeks { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$")]
        [Required]
        public string FirstName { get; set; }

        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$")]
        [StringLength(50)]
        [Required]
        public string LastName { get; set; }


#nullable enable
        [DataType(DataType.Date)]
        public DateTime? EnrollmentDate { get; set; }

#nullable enable
        public int? AcquiredCredits { get; set; }

#nullable enable
        public int? CurrentSemester { get; set; }

        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$")]
        [StringLength(25)]
#nullable enable
        public string? EducationLevel { get; set; }


#nullable enable
        public ICollection<Enrollment>? EnrolledOn { get; set; }

#nullable enable
        public string? filePath { get; set; }
    }
}
