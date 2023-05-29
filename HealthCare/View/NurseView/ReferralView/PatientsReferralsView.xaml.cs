using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.Service.ScheduleService;
using HealthCare.ViewModel.NurseViewModel;
using HealthCare.ViewModel.NurseViewModel.DataViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HealthCare.View.NurseView.ReferralView
{
    public partial class PatientsReferralsView : Window
    {
        private readonly SpecialistReferralService _specialistReferralService;
        private readonly DoctorService _doctorService;
        private ReferralListingViewModel _model;
        private Patient _patient;
        private ReferralViewModel? _referral;
        private readonly AppointmentService _appointmentService;
        private readonly Schedule _schedule;
        public PatientsReferralsView(Patient patient)
        {
            InitializeComponent();
            _model = new ReferralListingViewModel(patient);
            DataContext = _model;
            
            _schedule = Injector.GetService<Schedule>();

            _appointmentService = Injector.GetService<AppointmentService>();
            _specialistReferralService = Injector.GetService<SpecialistReferralService>();
            _doctorService = Injector.GetService<DoctorService>();

            _patient = patient;
            tbDate.SelectedDate = DateTime.Now;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lvReferrals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _referral= (ReferralViewModel) lvReferrals.SelectedItem;
        }

        private void btnMakeAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (_referral is null) {
                ViewUtil.ShowWarning("Izaberite upit koji zelite da iskoristite.");
                return;
            }

            Doctor referred = _doctorService.Get(_referral.SpecialistReferral.ReferredDoctorJMBG);

            if (!int.TryParse(tbHours.Text,out _) && !int.TryParse(tbMinutes.Text, out _))
            {
                ViewUtil.ShowWarning("Sati i minuti moraju biti brojevi");
                return;
            }

            int hours = int.Parse(tbHours.Text);
            int minutes = int.Parse(tbMinutes.Text);

            if (!tbDate.SelectedDate.HasValue)
            {
                ViewUtil.ShowWarning("Izaberite datum.");
                return;
            }

            DateTime selectedDate = tbDate.SelectedDate.Value;
            selectedDate = selectedDate.AddHours(hours);
            selectedDate = selectedDate.AddMinutes(minutes);

            if (selectedDate <= DateTime.Now)
            {
                ViewUtil.ShowWarning("Datum ne sme da bude u proslosti.");
                return;
            }

            TimeSlot slot = new TimeSlot(selectedDate, new TimeSpan(0,15,0));
            Appointment appointment = new Appointment(_patient.JMBG, referred.JMBG, slot ,false);

            if (!_schedule.CheckAvailability(referred.JMBG, _patient.JMBG, slot))
            {
                ViewUtil.ShowWarning("Doktor ili pacijent je zauzet u unetom terminu.");
                return;
            }
            _appointmentService.Add(appointment);
            ViewUtil.ShowInformation("Uspesno ste zakazali pregled.");

            var updated = _specialistReferralService.Get(_referral.SpecialistReferral.Id);
            updated.IsUsed = true;
            _specialistReferralService.Update(updated);
            
            _model.Update();
        }
    }
}
