﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Model;
using HealthCare.Service;

namespace HealthCare.ViewModel.NurseViewModel
{
    public class PostponableAppointmentsViewModel
    {
        public ObservableCollection<AppointmentViewModel> Appointments { get; set; }

        public PostponableAppointmentsViewModel(List<Appointment> postponable)
        {
            Appointments = new ObservableCollection<AppointmentViewModel>();

            for (int i = 0; i < Math.Min(5, postponable.Count); i++)
                    Appointments.Add(new AppointmentViewModel(
                        postponable[i],
                        Schedule.GetSoonestStartingTime(postponable[i])));

        }
    }
}