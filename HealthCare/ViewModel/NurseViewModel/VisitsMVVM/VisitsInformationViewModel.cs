using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View.NurseView.VisitsView;
using HealthCare.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCare.ViewModel.NurseViewModel.VisitsMVVM
{
    public class VisitsInformationViewModel:ViewModelBase
    {
        private Visit _visit;
        private readonly VisitService _visitService;
        public CancelCommand cancelCommand { get; set; }
        public RelayCommand visitCommand { get; set; }
        public VisitsInformationViewModel(Visit visit, Window window) {
            _visitService = Injector.GetService<VisitService>();
            _visit = visit;
            cancelCommand = new CancelCommand(window);
            visitCommand = new RelayCommand(o => {
                if (Temperature == 0 || SystolicPressure == 0 || DiastolicPressure == 0) {
                    ViewUtil.ShowWarning("Unesite polja.");
                    return;
                }

                if (Temperature < 0 || SystolicPressure < 0 || DiastolicPressure < 0) {
                    ViewUtil.ShowWarning("Vrednosti moraju biti pozitivni brojevi.");
                    return;
                }

                _visit.Temperature = Temperature;
                _visit.SystolicPressure = SystolicPressure;
                _visit.DiastolicPressure = DiastolicPressure;
                _visitService.Add(_visit);
                ViewUtil.ShowInformation("Uspesno ste obavili vizitu");
                window.Close();
            });
        }

        private double _temperature;
        public double Temperature
        {
            get => _temperature;
            set => _temperature = value;
        }

        private int _systolicPressure;
        public int SystolicPressure
        {
            get => _systolicPressure;
            set => _systolicPressure = value;
        }

        private int _diastolicPressure;
        public int DiastolicPressure
        {
            get => _diastolicPressure;
            set => _diastolicPressure = value;
        }

        private string _observations;
        public string Observations
        {
            get => _observations;
            set => _observations = value;
        }
    }
}
