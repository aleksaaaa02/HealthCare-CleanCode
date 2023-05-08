using System;
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
        public ObservableCollection<Appointment> Appointments { get; set; }

        public PostponableAppointmentsViewModel(List<Appointment> postponable)
        {
            Appointments = new ObservableCollection<Appointment>();

            if (postponable.Count >= 5)
            {
                for (int i = 0; i < 5; i++)
                    Appointments.Add(postponable[i]);
            }
            else
                Appointments = new ObservableCollection<Appointment>(postponable);
        }
    }
}
