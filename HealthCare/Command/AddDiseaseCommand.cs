using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.View.DoctorView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCare.Command
{
    public class AddDiseaseCommand : CommandBase
    {
        private readonly PatientInforamtionViewModel _viewModel;

        public AddDiseaseCommand(PatientInforamtionViewModel viewModel) 
        { 
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                _viewModel.AddDisease(_viewModel.Disease);
            }
            catch (ValidationException ve) {
                MessageBox.Show(ve.Message, "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
            }                  
        }
        private void Validate()
        {
            if (_viewModel.Disease == null)
            {
                throw new ValidationException("Morate uneti bolest u polje");  
            }
        }
    }
}
