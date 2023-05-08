using HealthCare.Exceptions;
using HealthCare.View.DoctorView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCare.Command
{
    public class RemoveDiseaseCommand : CommandBase
    {

        private readonly PatientInforamtionViewModel _view;
        public RemoveDiseaseCommand(PatientInforamtionViewModel view) {
            _view = view;
        }
        public override void Execute(object parameter)
        {
            try
            {
                string disease = _view.SelectedDisease;
                _view.RemoveDisease(disease);
            }
            catch (ValidationException ve)
            {
                MessageBox.Show(ve.Message, "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }
        private void Validate()
        {
            string disease = _view.SelectedDisease;
            if (disease == null)
            {
                throw new ValidationException("Morate odabrati bolest koju zelite da uklonite.");
            }
        }
    }
}
