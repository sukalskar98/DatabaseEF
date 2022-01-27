using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DatabaseEF.Models
{
    public interface IEmployeeLearningRepository
    {
        Task<EmployeeLearning> CreateEmployeeLearningAsync(EmployeeLearning employeeLearning);
        Task<IEnumerable<EmployeeLearning>> GetEmployeeLearningsAsync(int Id);
        Task<EmployeeLearning> EditEmployeeLearningAsync(EmployeeLearning employeeLearning);
        Task<EmployeeLearning> DeleteEmployeeLearningAsync(int id);
        Task<EmployeeLearning> ViewEmployeeLearningAsync(int id);
        List<SelectListItem> TopicsList();
    }
}
