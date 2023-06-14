using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.Service.ScheduleService;
using HealthCare.View;
using HealthCare.ViewModel.NurseViewModel.RoomsMVVM;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace HealthCare.ViewModel.NurseViewModel.TreatmantsReferralsMVVM
{
    public class TreatmantReferralViewModel : ViewModelBase
    {
        private readonly TreatmentReferralService _treatmentReferralService;
        public ObservableCollection<PatientsTreatmantRefarralsViewModel> Referrals { get; }

        private Treatment _treatment;
        private PatientsTreatmantRefarralsViewModel _selected;
        public PatientsTreatmantRefarralsViewModel Selected {
            get => _selected;
            set {
                _selected = value;
                if (_selected is not null)
                {
                    _treatment = new Treatment(0,value.Id,new TimeSlot(DateTime.Now, new TimeSpan(value.Days,0,0,0)));
                    loadRooms(_treatment);
                }
            }
        }

        private Patient _patient;
        public CancelCommand cancelCommand { get; set; }
        public RelayCommand makeTreatmantCommand { get; set; }
        public TreatmantReferralViewModel(Patient patient,Window window){
            _patient = patient;
            _treatmentReferralService = Injector.GetService<TreatmentReferralService>();
            Rooms = new ObservableCollection<RoomsViewModel>();
            Referrals = new ObservableCollection<PatientsTreatmantRefarralsViewModel>();

            cancelCommand = new CancelCommand(window);
            makeTreatmantCommand = new RelayCommand( o => {
                if (_treatment is null || SelectedRoom is null)
                {
                    ViewUtil.ShowWarning("Izaberite uput i sobu");
                    return;
                }

                _treatment.RoomId = SelectedRoom.Id;
                Injector.GetService<TreatmentService>().Add(_treatment);
                var updated = _treatmentReferralService.Get(_treatment.ReferralId);
                updated.IsUsed = true;
                _treatmentReferralService.Update(updated);
                loadReferrals();
            });

            loadReferrals();
        }

        public ObservableCollection<RoomsViewModel> Rooms { get; }
        public RoomsViewModel? SelectedRoom { get; set; }

        public void loadReferrals() {
            Referrals.Clear();
            Rooms.Clear();

            _treatmentReferralService
                .GetPatientsReferrals(_patient.JMBG)
                .ForEach(r => Referrals.Add(new PatientsTreatmantRefarralsViewModel(r)));
        }

        public void loadRooms(Treatment treatment)
        {
            var roomService = Injector.GetService<RoomService>();
            Rooms.Clear();

            Injector.GetService<RoomSchedule>()
                .GetAvailableRoomsByType(RoomType.PatientCare, treatment.TreatmentDuration)
                .ForEach(id => Rooms.Add(new RoomsViewModel(roomService.Get(id))));
        }
    }
}
