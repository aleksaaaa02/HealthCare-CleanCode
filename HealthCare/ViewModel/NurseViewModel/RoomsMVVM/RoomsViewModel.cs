using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Sockets;

namespace HealthCare.ViewModel.NurseViewModel.RoomsMVVM
{
    public class RoomsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RoomService roomService => Injector.GetService<RoomService>();
        private Room _room;
        public RoomsViewModel(Room room) {
            this.room = room;
            Id = room.Id;
            Name = room.Name;
            Type = ViewUtil.Translate(room.Type);
        }

        public Room room {
            get => _room;
            set {
                _room = value;
            }
        }

        private int _id;
        public int Id {
            get => _id;
            set { _id = value; }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; }
        }
        private string _type;
        public string Type
        {
            get => _type;
            set { _type = value; }
        }

        private ObservableCollection<Room> _rooms;

        public ObservableCollection<Room> Rooms {
            get => _rooms;
            set {
                if (_rooms != value) {
                    _rooms = value;
                }
            }
        }
    }
}
