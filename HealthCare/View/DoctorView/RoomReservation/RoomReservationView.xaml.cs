using HealthCare.Application;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.RoomReservation;
using System.Windows;

namespace HealthCare.View.DoctorView.RoomReservation
{
    public partial class RoomReservationView : Window
    {
        public RoomReservationView(Appointment appointment)
        {
            InitializeComponent();
            DataContext = new RoomReservationViewModel(appointment, this);
        }
    }
}
