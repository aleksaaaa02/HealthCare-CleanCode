using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using System.Windows;

namespace HealthCare.ViewModel.NurseViewModel.VisitsMVVM
{
    public class VisitsInformationViewModel : ViewModelBase
    {
        public CancelCommand CancelCommand { get; set; }
        public RelayCommand VisitCommand { get; set; }

        public VisitsInformationViewModel(Visit visit, Window window) {
            Observations = "";

            CancelCommand = new CancelCommand(window);
            VisitCommand = new RelayCommand(o => {
                if (!Validate()) return;
                visit.Temperature = Temperature;
                visit.SystolicPressure = SystolicPressure;
                visit.DiastolicPressure = DiastolicPressure;
                Injector.GetService<VisitService>().Add(visit);

                ViewUtil.ShowInformation("Uspesno ste obavili vizitu");
                window.Close();
            });
        }

        public double Temperature { get; set; }
        public int SystolicPressure { get; set; }
        public int DiastolicPressure { get; set; }
        public string Observations { get; set; }

        private bool Validate() {
            if (Temperature == 0 || SystolicPressure == 0 || DiastolicPressure == 0)
            {
                ViewUtil.ShowWarning("Unesite polja.");
                return false;
            }

            if (Temperature < 0 || SystolicPressure < 0 || DiastolicPressure < 0)
            {
                ViewUtil.ShowWarning("Vrednosti moraju biti pozitivni brojevi.");
                return false;
            }
            return true;
        }
    }
}
