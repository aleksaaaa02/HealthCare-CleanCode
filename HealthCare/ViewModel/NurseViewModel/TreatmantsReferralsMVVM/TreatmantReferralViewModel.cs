using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.Service.ScheduleService;
using HealthCare.View;
using HealthCare.ViewModel.NurseViewModel.RoomsMVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace HealthCare.ViewModel.NurseViewModel.TreatmantsReferralsMVVM
{
    public class TreatmantReferralViewModel : ViewModelBase
    {
        private ObservableCollection<PatientsTreatmantRefarralsViewModel> _referrals;
        private readonly TreatmentReferralService _treatmentReferralService;
        private readonly TreatmentService _treatmentService;
        public ObservableCollection<PatientsTreatmantRefarralsViewModel> Referrals
        {
            get => _referrals;
            set{_referrals = value;}
        }

        private PatientsTreatmantRefarralsViewModel _selected;
        private Treatment _treatment;
        public PatientsTreatmantRefarralsViewModel Selected {
            get => _selected;
            set {
                _selected = value;
                _treatment = new Treatment(0,value.Id,new TimeSlot(DateTime.Now, new TimeSpan(value.Days,0,0,0)));
                loadRooms(_treatment);
            }
        }

        private Patient _patient;
        public CancelCommand cancelCommand { get; set; }
        public RelayCommand makeTreatmantCommand { get; set; }
        public TreatmantReferralViewModel(Patient patient,Window window){
            _treatmentReferralService = Injector.GetService<TreatmentReferralService>();
            _treatmentService = Injector.GetService<TreatmentService>();
            _patient = patient;
            _roomSchedule = Injector.GetService<RoomSchedule>();
            Rooms = new ObservableCollection<RoomsViewModel>();
            Referrals = new ObservableCollection<PatientsTreatmantRefarralsViewModel>();
            cancelCommand = new CancelCommand(window);

            makeTreatmantCommand = new RelayCommand(o=> {
                if (_treatment == null || _treatment.RoomId == 0){
                    ViewUtil.ShowWarning("Izaberite uput i sobu");
                    return;
                }
                _treatmentService.Add(_treatment);
                TreatmentReferral updated = _treatmentReferralService.Get(_treatment.ReferralId);
                updated.IsUsed = true;
                _treatmentReferralService.Update(updated);
                loadReferrals();
            });

            loadReferrals();
        }

        private ObservableCollection<RoomsViewModel> _rooms;
        private readonly RoomSchedule _roomSchedule;
        public ObservableCollection<RoomsViewModel> Rooms
        {
            get => _rooms;
            set => _rooms = value;
        }
        private RoomsViewModel _selectedRoom;
        public RoomsViewModel SelectedRoom
        {
            get => _selectedRoom;
            set { 
                _selectedRoom = value; 
                _treatment.RoomId = value.Id;
            }
        }

        public void loadReferrals() {
            List<TreatmentReferral> referrals = _treatmentReferralService.GetPatientsReferrals(_patient.JMBG);
            Referrals.Clear();
            foreach (TreatmentReferral referral in referrals) {
                PatientsTreatmantRefarralsViewModel model = new PatientsTreatmantRefarralsViewModel(referral);
                Referrals.Add(model);
            }
        }

        public void loadRooms(Treatment treatment)
        {
            List<int> rooms = _roomSchedule.GetAvailableRoomsByType(RoomType.PatientCare,treatment.TreatmentDuration);
            RoomService roomService = Injector.GetService<RoomService>();
            Rooms.Clear();
            foreach (int room in rooms)
            {
                RoomsViewModel model = new RoomsViewModel(roomService.Get(room));
                Rooms.Add(model);
            }
       
        }

    }
}
