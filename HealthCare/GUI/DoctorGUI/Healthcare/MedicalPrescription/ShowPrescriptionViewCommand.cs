﻿using HealthCare.Core.Users.Model;
using HealthCare.GUI.Command;

namespace HealthCare.GUI.DoctorGUI.Healthcare.MedicalPrescription;

public class ShowPrescriptionViewCommand : CommandBase
{
    private readonly Patient _patient;

    public ShowPrescriptionViewCommand(Patient patient)
    {
        _patient = patient;
    }

    public override void Execute(object parameter)
    {
        new PrescriptionView(_patient).ShowDialog();
    }
}