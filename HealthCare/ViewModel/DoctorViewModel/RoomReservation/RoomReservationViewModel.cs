using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.RoomReservation.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel.RoomReservation
{
    public class RoomReservationViewModel : ViewModelBase
    {
        private ObservableCollection<RoomViewModel> _rooms;
        public IEnumerable<RoomViewModel> Rooms => _rooms;

        private RoomViewModel _selectedRoom;
        public RoomViewModel SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }

        private Hospital _hospital;



        public ICommand ReservateRoomCommand { get; }

        public RoomReservationViewModel(Hospital hospital, Appointment appointment, Window window) {
            _hospital = hospital;
            ReservateRoomCommand = new ReservateRoomCommand(hospital, window, this, appointment);
            LoadDataView(appointment);
        }
        public void LoadDataView(Appointment appointment)
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
            foreach(Room room in _hospital.RoomService.GetRoomsByType(roomType)) { 
                _rooms.Add(new RoomViewModel(room));
            }
        }

    }
}
