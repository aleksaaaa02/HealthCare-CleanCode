using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.Service.RenovationService;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class RenovationViewModel
    {
        public ObservableCollection<RoomViewModel> Items { get; }

        public RenovationViewModel()
        {
            Items = new ObservableCollection<RoomViewModel>();
            LoadAll();
        }

        public void LoadAll()
        {
            var rooms = Injector.GetService<RoomService>().GetAll();
            foreach (var room in rooms)
                Items.Add(new RoomViewModel(room));
        }
    }
}
