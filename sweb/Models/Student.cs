﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SWEB_app.Models
{
    public class Student
    {
        [Required]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [StringLength(10, MinimumLength = 3)]
        [Required]
        [Display(Name = "Student ID")]
        public string StudentID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public DateTime EnrolmentDate { get; set; }

        [Required]
        public int AcquiredCredits { get; set; }

        public int CurrentSemester { get; set; }

        [StringLength(25, MinimumLength = 3)]
        public string EducationLevel { get; set; }

        public ICollection<Enrollment> Courses { get; set; }

    }
}
