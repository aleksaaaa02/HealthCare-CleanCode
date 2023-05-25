using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel.NurseViewModel;
using HealthCare.ViewModel.NurseViewModel.DataViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HealthCare.View.NurseView.ReferralView
{
    public partial class PatientsReferralsView : Window
    {
        private ReferralListingViewModel _model;
        private Patient _patient;
        private ReferralViewModel? _referral;
        private Hospital _hospital;
        public PatientsReferralsView(Patient patient,Hospital hospital)
        {
            InitializeComponent();
            _model = new ReferralListingViewModel(patient,hospital);
            DataContext = _model;
            _patient = patient;
            _hospital = hospital;

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
                Utility.ShowWarning("Izaberite upit koji zelite da iskoristite.");
                return;
            }

            Doctor referred = _hospital.DoctorService.Get(_referral.SpecialistReferral.ReferredDoctorJMBG);

            if (!int.TryParse(tbHours.Text,out _) && !int.TryParse(tbMinutes.Text, out _))
            {
                Utility.ShowWarning("Sati i minuti moraju biti brojevi");
                return;
            }

            int hours = int.Parse(tbHours.Text);
            int minutes = int.Parse(tbMinutes.Text);

            if (!tbDate.SelectedDate.HasValue)
            {
                Utility.ShowWarning("Izaberite datum.");
                return;
            }

            DateTime selectedDate = tbDate.SelectedDate.Value;
            selectedDate = selectedDate.AddHours(hours);
            selectedDate = selectedDate.AddMinutes(minutes);

            TimeSlot slot = new TimeSlot(selectedDate, new TimeSpan(0,15,0));
            Appointment appointment = new Appointment(_patient, referred, slot ,false);

            if (!Schedule.CreateAppointment(appointment))
            {
                Utility.ShowWarning("Doktor ili pacijent je zauzet u unetom terminu.");
                return;
            }

            Utility.ShowInformation("Uspesno ste zakazali pregled.");

            var updated = _hospital.SpecialistReferralService.Get(_referral.SpecialistReferral.Id);
            updated.IsUsed = true;
            _hospital.SpecialistReferralService.Update(updated);
            
            _model.Update();
        }
    }
}
