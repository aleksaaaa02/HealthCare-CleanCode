using System.ComponentModel;
using HealthCare.Application;
using HealthCare.Service;
using HealthCare.Model;
using System.Collections.ObjectModel;
using System.Windows;

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
                MessageBox.Show("a");
                _id = _treatmentReferral.Id;
            }
        }

        private int _days;
        public int Days {
            get => _days;
            set {
                _days = _treatmentReferral.DaysOfTreatment;
            }
        }

        private string _doctor;
        public string Doctor {
            get => _doctor;
            set {
                _doctor = doctorService.Get(_treatmentReferral.DoctorJMBG).Name + " " 
                     + doctorService.Get(_treatmentReferral.DoctorJMBG).LastName;
            }
        }

        private string _therapy;
        public string Therapy
        {
            get => _therapy;
            set {
                _therapy = string.Join(",", therapyService.Get(_treatmentReferral.TherapyID).InitialMedication);
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
