using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class RenovationViewModel
    {
        public ObservableCollection<RoomViewModel> Items { get; }

        public RenovationViewModel(RoomService roomService)
        {
            Items = new ObservableCollection<RoomViewModel>();

            LoadAll(roomService.GetAll());
        }

        public void LoadAll(List<Room> rooms)
        {
            foreach (var room in rooms)
                Items.Add(new RoomViewModel(room));
        }
    }
}
