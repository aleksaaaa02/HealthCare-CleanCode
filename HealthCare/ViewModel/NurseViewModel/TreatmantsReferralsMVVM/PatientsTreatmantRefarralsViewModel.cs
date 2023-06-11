using System.ComponentModel;
using HealthCare.Application;
using HealthCare.Service;
using HealthCare.Model;
using System.Collections.ObjectModel;

namespace HealthCare.ViewModel.NurseViewModel.TreatmantsReferralsMVVM
{
    public class PatientsTreatmantRefarralsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public TreatmentService treatmentService => Injector.GetService<TreatmentService>();
        public DoctorService doctorService => Injector.GetService<DoctorService>();
        public TherapyService therapyService => Injector.GetService<TherapyService>();

        private TreatmentReferral _treatmentReferral;
        public PatientsTreatmantRefarralsViewModel(TreatmentReferral referral) {
            treatmentReferral = referral;
            Id = referral.Id;
            Days = referral.DaysOfTreatment;
            Doctor = doctorService.Get(referral.DoctorJMBG).Name + " "
                     + doctorService.Get(referral.DoctorJMBG).LastName;
            Therapy = string.Join(",", therapyService.Get(referral.TherapyID).InitialMedication);
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
            set {
                _days = value;
            }
        }

        private string _doctor;
        public string Doctor {
            get => _doctor;
            set {
                _doctor = value;
            }
        }

        private string _therapy;
        public string Therapy
        {
            get => _therapy;
            set {
                _therapy = value;
            }
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
