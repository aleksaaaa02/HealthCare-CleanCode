using HealthCare.Model;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class SurveyService : NumericService<Survey>
    {
        public SurveyService(IRepository<Survey> repository) : base(repository)
        {
        }

        public List<Survey> GetForUser(string userJmbg)
        {
            return GetAll().FindAll(x => x.DoctorJMBG == userJmbg);
        }

        public List<Survey> GetForTopic(string topicName)
        {
            return GetAll().FindAll(x => x.Description.Equals(topicName));
        }

        public List<Survey> GetHospitalSurveys()
        {
            return GetForUser("");
        }
    }
}
