using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using HealthCare.ViewModel.DoctorViewModel.RoomReservation.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel.RoomReservation
{
    public class RoomReservationViewModel : ViewModelBase
    {
        private readonly RoomService _roomService;
        private ObservableCollection<RoomViewModel> _rooms;
        private RoomViewModel _selectedRoom;

        public IEnumerable<RoomViewModel> Rooms => _rooms;
        public ICommand ReservateRoomCommand { get; }

        public RoomViewModel SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }

        public RoomReservationViewModel(Appointment appointment, Window window) {
            _roomService = (RoomService)ServiceProvider.services["RoomService"];
            ReservateRoomCommand = new ReserveRoomCommand(window, this, appointment);
            Update(appointment);
        }

        public void Update(Appointment appointment)
        {
            _rooms = new ObservableCollection<RoomViewModel>();
            if (appointment.IsOperation)
            {
                LoadRooms(RoomType.Operational);
            }
            else
            {
                LoadRooms(RoomType.Examinational);
            }

        }
        private void LoadRooms(RoomType roomType)
        {
            _rooms.Clear();
            foreach(Room room in _roomService.GetRoomsByType(roomType)) { 
                _rooms.Add(new RoomViewModel(room));
            }
        }

    }
}
