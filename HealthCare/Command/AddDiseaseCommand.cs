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
        private Patient _selectedPatient;
        private readonly PatientInforamtionViewModel _viewModel;

        public AddDiseaseCommand(Patient patient, PatientInforamtionViewModel viewModel) 
        { 
            _selectedPatient = patient;
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            string newDisease = _viewModel.Disease;

            if (newDisease == null)
            {
                MessageBox.Show("Morate uneti bolest u polje", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _selectedPatient.MedicalRecord.MedicalHistory = _selectedPatient.MedicalRecord.MedicalHistory.Concat(new string[] { newDisease }).ToArray();
            _viewModel.Update();
        }
    }
}
