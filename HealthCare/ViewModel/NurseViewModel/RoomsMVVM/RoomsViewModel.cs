using HealthCare.Core.Interior;
using HealthCare.View;

namespace HealthCare.ViewModel.NurseViewModel.RoomsMVVM
{
    public class RoomsViewModel
    {
        private readonly Room _room;

        public RoomsViewModel(Room room)
        {
            _room = room;
        }

        public int Id => _room.Id;
        public string Name => _room.Name;
        public string Type => ViewUtil.Translate(_room.Type);
    }
}