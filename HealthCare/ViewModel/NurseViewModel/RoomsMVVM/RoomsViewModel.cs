using HealthCare.Model;
using HealthCare.View;

namespace HealthCare.ViewModel.NurseViewModel.RoomsMVVM
{
    public class RoomsViewModel
    {
        public RoomsViewModel(Room room) {
            _room = room;
        }

        private readonly Room _room;
        public int Id => _room.Id;
        public string Name => _room.Name;
        public string Type => ViewUtil.Translate(_room.Type);
    }
}
