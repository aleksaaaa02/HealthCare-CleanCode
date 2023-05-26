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
        private readonly SpecialistReferralService _specialistReferralService;
        private readonly DoctorService _doctorService;
        private ReferralListingViewModel _model;
        private Patient _patient;
        private ReferralViewModel? _referral;
        public PatientsReferralsView(Patient patient)
        {
            InitializeComponent();
            _model = new ReferralListingViewModel(patient);
            DataContext = _model;

            _specialistReferralService = (SpecialistReferralService)ServiceProvider.services["SpecialistReferralService"];
            _doctorService = (DoctorService)ServiceProvider.services["DoctorService"];

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
                Utility.ShowWarning("Izaberite upit koji zelite da iskoristite.");
                return;
            }

            Doctor referred = _doctorService.Get(_referral.SpecialistReferral.ReferredDoctorJMBG);

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

            var updated = _specialistReferralService.Get(_referral.SpecialistReferral.Id);
            updated.IsUsed = true;
            _specialistReferralService.Update(updated);
            
            _model.Update();
        }
    }
}
