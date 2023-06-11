using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Exceptions;
using HealthCare.Service;
using HealthCare.ViewModel.ManagerViewModel.Command;
using HealthCare.ViewModel.ManagerViewModel.DataViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class AbsenceRequestListingViewModel : ViewModelBase
    {
        public ICommand ApproveRequestCommand { get; }
        public ICommand DeclineRequestCommand { get; }
        public ICommand ExitCommand { get; }
        public ObservableCollection<AbsenceRequestViewModel> Items { get; }

        private bool _areApproved = false;
        public bool AreApproved 
        {
            get => _areApproved;
            set 
            {
                _areApproved = value;
                Load();
            }
        }

        public AbsenceRequestViewModel? SelectedRequest { get; set; }

        public AbsenceRequestListingViewModel(Window view) 
        {
            Items = new ObservableCollection<AbsenceRequestViewModel>();

            ApproveRequestCommand = new ApproveRequestCommand(this);
            DeclineRequestCommand = new DeclineRequestCommand(this);
            ExitCommand = new CancelCommand(view);
            Load();
        }

        private void Load()
        {
            Items.Clear();
            Injector.GetService<AbsenceRequestService>()
                .GetAll().Where(r => r.IsApproved == AreApproved)
                .Select(r => new AbsenceRequestViewModel(r))
                .ToList().ForEach(model => Items.Add(model));
        }
    }
}
