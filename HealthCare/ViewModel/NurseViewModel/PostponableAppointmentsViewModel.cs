using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service.ScheduleService;
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
            Schedule _schedule = Injector.GetService<Schedule>();
            Appointments = new ObservableCollection<AppointmentViewModel>();

            for (int i = 0; i < Math.Min(5, postponable.Count); i++)
                    Appointments.Add(new AppointmentViewModel(
                        postponable[i],
                        _schedule.GetSoonestTimeSlot(postponable[i]).Start));

        }
    }
}