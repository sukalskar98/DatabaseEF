using DatabaseEF.Helper;
using DataModels.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEF.Models
{
    public class EmployeeLearningRepository : IEmployeeLearningRepository
    {
        private readonly EmployeeNewDBContext context;

        EmployeeLearningAPI _api = new EmployeeLearningAPI();

        public EmployeeLearningRepository(EmployeeNewDBContext context)
        {
            this.context = context;
        }


        public async Task<EmployeeLearning> CreateEmployeeLearningAsync(EmployeeLearning employeeLearning)
        {
            EmployeeLearning learning = new EmployeeLearning();
            HttpClient client = _api.Initial();

            var topics = context.Topics;
            int topicId = 0;
            foreach(var topic in topics)
            {
                if (topic.TopicName.Equals(employeeLearning.TopicName))
                {
                    topicId = topic.Id;
                }
            }
            if(employeeLearning.Progress>0 && employeeLearning.Progress<=100)
            {
                employeeLearning.Status = StatusEnum.InProgress;
                employeeLearning.ActualEndDate = employeeLearning.EndDate;
            }
            else if (employeeLearning.Progress == 100)
            {
                employeeLearning.Status = StatusEnum.Complete;
                employeeLearning.EndDate=DateTime.Now;
                employeeLearning.ActualEndDate = DateTime.Now;
            }
            else
            {
                employeeLearning.Status = StatusEnum.New;
                employeeLearning.ActualEndDate = employeeLearning.EndDate;
            }
            var empLearnObj = new EmployeeLearning
            {
                TopicName = employeeLearning.TopicName,
                StartDate = employeeLearning.StartDate,
                EndDate = employeeLearning.EndDate,
                Progress = employeeLearning.Progress,
                Status = employeeLearning.Status,
                ActualEndDate = employeeLearning.ActualEndDate,
                TopicId = topicId,
                EmployeeId = employeeLearning.EmployeeId
            };
            var newPostJson = JsonConvert.SerializeObject(empLearnObj);
            var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PostAsync("api/EmployeeLearning/addlearning", payload);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                learning = JsonConvert.DeserializeObject<EmployeeLearning>(result);
            }
            return learning;
        }

        public async Task<EmployeeLearning> ViewEmployeeLearningAsync(int id)
        {
            var learning = new EmployeeLearning();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/EmployeeLearning/learning/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                learning = JsonConvert.DeserializeObject<EmployeeLearning>(result);
            }
            return learning;
        }

        public async Task<EmployeeLearning> DeleteEmployeeLearningAsync(int id)
        {
            var learning = new EmployeeLearning();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/EmployeeLearning/deletelearning/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                learning = JsonConvert.DeserializeObject<EmployeeLearning>(result);
            }
            return learning;
        }

        public async Task<EmployeeLearning> EditEmployeeLearningAsync(EmployeeLearning employeeLearning)
        {
            var learning = new EmployeeLearning();
            if (employeeLearning.Progress > 0 && employeeLearning.Progress < 100)
            {
                employeeLearning.Status = StatusEnum.InProgress;
                employeeLearning.ActualEndDate = employeeLearning.EndDate;
            }
            else if (employeeLearning.Progress == 100)
            {
                employeeLearning.Status = StatusEnum.Complete;
                employeeLearning.EndDate = DateTime.Now;
                employeeLearning.ActualEndDate = DateTime.Now;
            }
            else
            {
                employeeLearning.Status = StatusEnum.New;
                employeeLearning.ActualEndDate = employeeLearning.EndDate;
            }
            HttpClient client = _api.Initial();
            var newPutJson = JsonConvert.SerializeObject(employeeLearning);
            var payload = new StringContent(newPutJson, Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PutAsync($"api/EmployeeLearning/editlearning/{employeeLearning.Id}", payload);
            if(res.IsSuccessStatusCode)
            {
                var result= res.Content.ReadAsStringAsync().Result;
                learning =  JsonConvert.DeserializeObject<EmployeeLearning>(result);
            }
            return learning;
        }

        public async Task<IEnumerable<EmployeeLearning>> GetEmployeeLearningsAsync(int Id)
        {
            List<EmployeeLearning> learnings = new List<EmployeeLearning>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/EmployeeLearning/{Id}");
            if(res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                learnings = JsonConvert.DeserializeObject<List<EmployeeLearning>>(result);
            }
            return learnings;
        }

        public List<SelectListItem> TopicsList()
        {
            List<SelectListItem> topicslist = new List<SelectListItem>();
            var topics = context.Topics;
            foreach(var topic in topics)
            {
                topicslist.Add(new SelectListItem(topic.TopicName, topic.TopicName));
            }
            return topicslist;
        }
    }
}
