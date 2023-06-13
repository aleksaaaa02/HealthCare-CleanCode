using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using HealthCare.View.NurseView.VisitsView;
using HealthCare.ViewModel.NurseViewModel.TreatmantsReferralsMVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace HealthCare.ViewModel.NurseViewModel.VisitsMVVM
{
    public class VisitsViewModel : ViewModelBase
    {
        private ObservableCollection<TreatmentsViewModel> _treatments;
        private readonly TreatmentService _treatmentService;
        private readonly VisitService _visitService;
        public ObservableCollection<TreatmentsViewModel> Treatments {
            get => _treatments;
            set => _treatments = value;
        }

        private TreatmentsViewModel _selected;
        private Visit _visit;
        public TreatmentsViewModel Selected {
            get => _selected;
            set {
                _selected = value;
                if (_selected is not null)
                    _visit = new Visit(0, 0, 0, "", DateTime.Now, _selected.Id);
            }
        }

        private string _filter;
        public string Filter {
            get => _filter;
            set { 
                _filter = value;
                filterTable(value);
            }
        }

        public CancelCommand cancelCommand { get; set; }
        public RelayCommand visitCommand { get; set; }
        public VisitsViewModel(Window window) {
            _treatmentService = Injector.GetService<TreatmentService>();
            _visitService = Injector.GetService<VisitService>();
            Treatments = new ObservableCollection<TreatmentsViewModel>();
            cancelCommand = new CancelCommand(window);
            visitCommand = new RelayCommand(o => {
                if (_selected == null) {
                    ViewUtil.ShowWarning("Izaberite pacijenta.");
                    return;
                }
                new VisitInformationView(_visit).ShowDialog();
                loadTreatments();

            });
            loadTreatments();
        }

        public List<TreatmentsViewModel> starting = new List<TreatmentsViewModel>();
        public void loadTreatments() {
            List<Visit> done = _visitService.getVisits();
            List<Treatment> treatments = _treatmentService.getCurrent().Where(x => done.All(v => v.TreatmentId != x.Id)).ToList();
            Treatments.Clear();
            foreach (Treatment treatment in treatments) {
                TreatmentsViewModel model = new TreatmentsViewModel(treatment);
                Treatments.Add(model);
                starting.Add(model);
            }
        }

        public void filterTable (string filter) {
            IEnumerable<TreatmentsViewModel> query = starting.ToList().Where(
                x =>
                    x.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)||
                    x.LastName.Contains(filter, StringComparison.OrdinalIgnoreCase)||
                    x.RoomName.Contains(filter, StringComparison.OrdinalIgnoreCase)
                
                ).ToList();

            update((List<TreatmentsViewModel>) query);
        }

        public void update(List<TreatmentsViewModel> query) { 
            Treatments.Clear();
            foreach (TreatmentsViewModel model in query) {
                Treatments.Add(model);
            }
        }

    }
}
