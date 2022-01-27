using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DatabaseEF.Models
{
    public class ListRepository: IListRepository
    {
        private readonly EmployeeNewDBContext dBContext;
        public ListRepository(EmployeeNewDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public Lists list()
        {
            return new Lists(dBContext); 
        }
    }
}
