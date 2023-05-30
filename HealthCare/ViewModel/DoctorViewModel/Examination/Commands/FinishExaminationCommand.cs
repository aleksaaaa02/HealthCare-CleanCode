using HealthCare.Command;
using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View.DoctorView;
using System.Windows;

namespace HealthCare.ViewModel.DoctorViewModel.Examination.Commands
{
    public class FinishExaminationCommand : CommandBase
    {
        private readonly AnamnesisService _anamnesisService;
        private readonly Window _window;
        private readonly DoctorExamViewModel _viewModel;
        private readonly Appointment _appointment;
        private readonly int _roomId;

        public FinishExaminationCommand(Window window, Appointment appointment, DoctorExamViewModel viewModel) 
        {
            _anamnesisService = Injector.GetService<AnamnesisService>();
            _viewModel = viewModel;
            _window = window;
            _appointment = appointment;
            _roomId = appointment.RoomID;
        }
        public override void Execute(object parameter)
        {
            _window.Close();

            string conclusion = _viewModel.Conclusion;
            Anamnesis anamnesis = _anamnesisService.Get(_appointment.AnamnesisID);
            anamnesis.DoctorsObservations = conclusion;
            _anamnesisService.Update(anamnesis);

            new UsedDynamicEquipmentView(_roomId).Show();
        }
    }
}
