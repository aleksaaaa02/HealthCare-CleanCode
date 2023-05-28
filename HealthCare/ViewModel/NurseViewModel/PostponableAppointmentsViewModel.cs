﻿using HealthCare.Model;
using HealthCare.Service.ScheduleTest;
using HealthCare.ViewModel.NurseViewModel.DataViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HealthCare.ViewModel.NurseViewModel
{
    public class PostponableAppointmentsViewModel
    {
        public ObservableCollection<AppointmentViewModel> Appointments { get; set; }

        public PostponableAppointmentsViewModel(List<Appointment> postponable)
        {
            TestSchedule _schedule = new TestSchedule();
            Appointments = new ObservableCollection<AppointmentViewModel>();

            for (int i = 0; i < Math.Min(5, postponable.Count); i++)
                    Appointments.Add(new AppointmentViewModel(
                        postponable[i],
                        _schedule.GetSoonestStartingTime(postponable[i])));

        }
    }
}