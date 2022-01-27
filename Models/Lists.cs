using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DatabaseEF.Models
{
    public class Lists
    {
        private readonly EmployeeNewDBContext dBContext;
        public Lists(EmployeeNewDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public List<SelectListItem> DepartmentList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach(var dept in dBContext.Departments)
            {
                list.Add(new SelectListItem(dept.DepartmentName, dept.DepartmentName));
            }
            return list;
        }

        public List<SelectListItem> JobList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var job in dBContext.Jobs)
            {
                list.Add(new SelectListItem(job.JobTitle, job.JobTitle));
            }
            return list;
        }
    }
}
