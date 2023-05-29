using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Model.Renovation;
using HealthCare.Service;
using HealthCare.Service.RenovationService;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using System;
using System.Collections.Generic;
using System.Windows;

namespace HealthCare.View.ManagerView.RenovationView
{
    public partial class JoiningRenovationView : Window
    {
        private readonly JoiningRenovationService _joiningRenovationService;
        private readonly TimeSlot _scheduled;
        private readonly int _room1, _room2;

        public JoiningRenovationView(List<RoomViewModel> rooms, TimeSlot scheduled)
        {
            InitializeComponent();

            _joiningRenovationService = Injector.GetService<JoiningRenovationService>();

            _scheduled = scheduled;
            _room1 = rooms[0].RoomId;
            _room2 = rooms[1].RoomId;

            lvRooms.ItemsSource = rooms;
            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            foreach (RoomType a in Enum.GetValues(typeof(RoomType)))
                cbType.Items.Add(ViewUtil.Translate(a));
            cbType.SelectedIndex = 0;
        }

        private void btnRenovate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                ViewUtil.ShowWarning("Naziv sobe ne sme da bude prazan.");
                return;
            }

            var name = tbName.Text.Trim();
            var typeIndex = cbType.SelectedIndex;
            var resultRoom = new Room(0, name, (RoomType)typeIndex);

            _joiningRenovationService.Add(new JoiningRenovation(_room1, _scheduled, _room2, resultRoom));
            ViewUtil.ShowInformation("Uspesno zakazano renoviranje 2 sobe.");
            Close();
        }

        private void btnExist_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
