using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HealthCare.ViewModel.NurseViewModel.VisitsMVVM
{
    public class TreatmentsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public TreatmentService treatmentService => Injector.GetService<TreatmentService>();
        public Treatment _treatment;
        public PatientService patientService => Injector.GetService<PatientService>();
        public TreatmentReferralService treatmentReferralService => Injector.GetService<TreatmentReferralService>();
        public RoomService roomService => Injector.GetService<RoomService>();
        private Patient _patient;
        public TreatmentsViewModel(Treatment treatment) {
            this.treatment = treatment;
            Id = treatment.Id;
            _patient = patientService.Get(treatmentReferralService.Get(treatment.ReferralId).PatientJMBG);
            Name = _patient.Name;
            LastName = _patient.LastName;
            RoomName = roomService.Get(treatment.RoomId).Name;
        }

        public Treatment treatment { 
            get => _treatment;
            set => _treatment = value;
        }

        private int _id;
        public int Id
        {
            get => _id;
            set => _id = value; 
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        private string _roomName;
        public string RoomName
        {
            get => _roomName;
            set => _roomName = value;
        }

        private ObservableCollection<Treatment> _treatments;
        public ObservableCollection<Treatment> Treatments {
            get => _treatments;
            set {
                if (_treatments != value) {
                    _treatments = value;
                }
            }
        }
    }
}
