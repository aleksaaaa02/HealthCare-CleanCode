﻿using HealthCare.Model;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            return GetAll().FindAll(x => x.TopicName.Equals(topicName));
        }

        public double GetAverageDoctor(string userJMBG)
        {
            List<Survey> list = GetForUser(userJMBG);
            int counter = 0;
            double totalRating = 0;
            foreach (Survey survey in list) 
            {
                if (survey.SelectedRating != null)
                {
                    counter++;
                    totalRating += (double)survey.SelectedRating;
                }
            }
            if (counter == 0 || totalRating == 0) return 0;
            return Math.Round(totalRating / counter,2);
        }

    }
}
