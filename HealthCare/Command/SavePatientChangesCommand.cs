using HealthCare.Context;
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
    public class SavePatientChangesCommand : CommandBase
    {
        private readonly PatientInforamtionViewModel _viewModel;
        private Hospital _hospital;
        private Patient _selectedPatient;
        public SavePatientChangesCommand(Hospital hospital, Patient patient, PatientInforamtionViewModel viewModel) {
            _hospital = hospital;
            _viewModel = viewModel;
            _selectedPatient = patient;
        }
        public override void Execute(object parameter)
        {
            _selectedPatient.MedicalRecord.MedicalHistory = _viewModel.PreviousDisease.ToArray();
            _hospital.PatientService.Update(_selectedPatient);
            
            MessageBox.Show("Pacijent uspesno sacuvan!", "Obavestenje", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
