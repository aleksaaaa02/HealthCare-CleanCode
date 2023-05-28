﻿using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.Service.RenovationService;
using HealthCare.View.ManagerView.RenovationView;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using HealthCare.ViewModel.ManagerViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HealthCare.View.ManagerView
{
    /// <summary>
    /// Interaction logic for RenovationListingView.xaml
    /// </summary>
    public partial class RenovationListingView : Window
    {
        private readonly Hospital _hospital;
        private readonly Window _parent;
        private readonly RoomService _roomService;
        private readonly SimpleRenovationService _renovationService;

        public RenovationListingView(Window parent, Hospital hospital)
        {
            InitializeComponent();

            _hospital = hospital;
            _parent = parent;
            _roomService = hospital.RoomService;
            _renovationService = hospital.SimpleRenovationService;

            DataContext = new RenovationViewModel(_roomService);
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            _parent.Show();
            Close();
        }

        private void Button_Renovate(object sender, RoutedEventArgs e)
        {
            try {
                Validate();
            }
            catch (ValidationException ve) {
                Utility.ShowWarning(ve.Message);
                return;
            }

            var selected = GetSelectedIds();
            lvRooms.SelectedItems.Clear();

            if (GetMaxSelectionCount() == 1) {
                // zakazi renoviranje za jednu sobu
                Utility.ShowInformation("Uspesno zakazano renoviranje sobe.");
                return;
            }

            if (selected.Count == 1)
                new SplittingRenovationView(_hospital, selected[0]).ShowDialog();
            else
                new JoiningRenovationView(_hospital, selected).ShowDialog();
        }

        private void Validate()
        {
            var selected = lvRooms.SelectedItems;

            if (selected.Count == 0)
                throw new ValidationException("Izaberite sobu/sobe za renoviranje.");

            var startDate = StartDatePicker.SelectedDate;
            var endDate = EndDatePicker.SelectedDate;

            if (startDate is null || endDate is null)
                throw new ValidationException("Nisu izabrani datum pocetka i kraja renoviranja.");

            var slot = new TimeSlot((DateTime)startDate, (DateTime)endDate);

            foreach (RoomViewModel room in selected)
                ValidateRoom(room, slot);
        }

        private void ValidateRoom(RoomViewModel room, TimeSlot slot)
        {
            var id = room.RoomId;
            if (!_renovationService.RoomFree(id, slot))
                throw new ValidationException(
                    string.Format("Soba sa ID-jem {0} nije slobodna u izabranom terminu.", id));
        }

        private List<int> GetSelectedIds()
        {
            var ids = new List<int>();
            foreach (RoomViewModel room in lvRooms.SelectedItems)
                ids.Add(room.RoomId);
            return ids;
        }

        private void lvRooms_SelectionChanged(object sender, EventArgs e)
        {
            var selected = lvRooms.SelectedItems;
            while (selected.Count > GetMaxSelectionCount())
                selected.RemoveAt(selected.Count-1);
        }

        private int GetMaxSelectionCount()
        {
            return cbComplex.IsChecked ?? false ? 2 : 1;
        }
    }
}
