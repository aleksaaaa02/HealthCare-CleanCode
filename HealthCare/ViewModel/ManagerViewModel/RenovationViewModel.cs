﻿using System.Collections.ObjectModel;
using HealthCare.Application;
using HealthCare.Service;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class RenovationViewModel
    {
        public RenovationViewModel()
        {
            Items = new ObservableCollection<RoomViewModel>();
            LoadAll();
        }

        public ObservableCollection<RoomViewModel> Items { get; }

        public void LoadAll()
        {
            var rooms = Injector.GetService<RoomService>().GetAll();
            foreach (var room in rooms)
                Items.Add(new RoomViewModel(room));
        }
    }
}