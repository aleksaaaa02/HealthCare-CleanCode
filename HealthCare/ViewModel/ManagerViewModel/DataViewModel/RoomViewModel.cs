using HealthCare.Core.Interior;
using HealthCare.View;

namespace HealthCare.ViewModel.ManagerViewModel.DataViewModel;

public class RoomViewModel : ViewModelBase
{
    private readonly Room _room;

    public RoomViewModel(Room room)
    {
        _room = room;
    }

    public int RoomId => _room.Id;
    public string RoomName => _room.Name;
    public string RoomType => ViewUtil.Translate(_room.Type);
}