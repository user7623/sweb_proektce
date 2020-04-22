using Microsoft.AspNetCore.Mvc.Rendering;
using SWEB_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sweb.Models
{
    public class TeacherViewModel
    {
        public List<Teacher> Teachers { get; set; }

        public SelectList Offices { get; set; }
        public string CourseSemester { get; set; }
        public string SearchName { get; set; }
        public string SearchLastName { get; set; }
        public string SearchAcademicRank { get; set; }

        public string SearchEducation { get; set; }

    }
}
