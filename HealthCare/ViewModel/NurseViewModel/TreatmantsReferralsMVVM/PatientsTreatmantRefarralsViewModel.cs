using System.ComponentModel;
using HealthCare.Application;
using HealthCare.Service;
using HealthCare.Model;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.ViewModel.NurseViewModel.TreatmantsReferralsMVVM
{
    public class PatientsTreatmantRefarralsViewModel
    {
        public PatientsTreatmantRefarralsViewModel(TreatmentReferral referral) {
            var doctorService = Injector.GetService<DoctorService>();
            var therapyService = Injector.GetService<TherapyService>();
            var medicationService = Injector.GetService<MedicationService>();

            var doctor = doctorService.Get(referral.DoctorJMBG);

            Id = referral.Id;
            Days = referral.DaysOfTreatment;
            Doctor = doctor.Name + " " + doctor.LastName;
            var medications = therapyService.Get(referral.TherapyID)
                .InitialMedication.Select(id => medicationService.Get(id).Name);
            Therapy = string.Join(",", medications);
        }

        public int Id { get; set; }
        public int Days { get; set; }
        public string Doctor { get; set; }
        public string Therapy { get; set; }
    }
}
