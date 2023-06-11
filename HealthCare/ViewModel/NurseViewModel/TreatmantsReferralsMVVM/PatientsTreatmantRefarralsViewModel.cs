using System.ComponentModel;
using HealthCare.Application;
using HealthCare.Service;
using HealthCare.Model;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace HealthCare.ViewModel.NurseViewModel.TreatmantsReferralsMVVM
{
    public class PatientsTreatmantRefarralsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public TreatmentService treatmentService => Injector.GetService<TreatmentService>();
        public DoctorService doctorService => Injector.GetService<DoctorService>();
        public TherapyService therapyService => Injector.GetService<TherapyService>();
        public MedicationService medicationService => Injector.GetService<MedicationService>();

        private TreatmentReferral _treatmentReferral;
        public PatientsTreatmantRefarralsViewModel(TreatmentReferral referral) {
            _treatmentReferral = referral;
            _id = referral.Id;
            _days = referral.DaysOfTreatment;
            _doctor = doctorService.Get(referral.DoctorJMBG).Name + " "
                     + doctorService.Get(referral.DoctorJMBG).LastName;
            List<string> medications = new List<string>();
            foreach (int id in therapyService.Get(referral.TherapyID).InitialMedication)
                medications.Add(medicationService.Get(id).Name);

            _therapy = string.Join(",", medications);
        }

        public TreatmentReferral treatmentReferral {
            get => _treatmentReferral;
            set {
                _treatmentReferral = value;
            }
        }

        private int _id;
        public int Id {
            get => _id;
            set {
                _id = value;
            }
        }

        private int _days;
        public int Days {
            get => _days;
            set { _days = value;}
        }

        private string _doctor;
        public string Doctor {
            get => _doctor;
            set {_doctor = value;}
        }

        private string _therapy;
        public string Therapy
        {
            get => _therapy;
            set {_therapy = value;}
        }

        private ObservableCollection<TreatmentReferral> _referrals;
        public ObservableCollection<TreatmentReferral> Refferals {
            get => _referrals;
            set {
                if (_referrals != value) {
                    _referrals = value;
                }
            }
        }

    }
}
