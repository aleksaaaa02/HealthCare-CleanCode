using HealthCare.Model;
using HealthCare.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel.DoctorViewModel.RoomReservation
{
    public class RoomViewModel : ViewModelBase
    {
        private readonly Room _room;
        public string RoomName => _room.Name;
        public int RoomId => _room.Id;
        public string RoomType => ViewUtil.Translate(_room.Type);


        public RoomViewModel(Room room) {
            _room = room;
        }
    }
}
