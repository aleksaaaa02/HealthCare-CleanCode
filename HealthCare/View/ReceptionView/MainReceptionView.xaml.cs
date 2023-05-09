﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View.PatientView;

namespace HealthCare.View.ReceptionView
{
    public partial class MainReceptionView : Window 
    {
        private readonly Hospital hospital;
        public MainReceptionView(Hospital hospital)
        {
            InitializeComponent();
            this.hospital = hospital;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string JMBG = tbJMBG.Text.Trim();

            Patient? patient = hospital.PatientService.GetAccount(JMBG);

            if(patient == null)
            {
                CreatePatientView createPatientView = new CreatePatientView(hospital,JMBG);
                createPatientView.ShowDialog();
            }
            else
            {
                Appointment? starting = Schedule.GetStartingAppointment(JMBG);
                if (starting == null)
                {
                    ShowErrorMessageBox();
                    return;
                }
                NurseAnamnesisView anamnesisView = new NurseAnamnesisView(hospital,starting.AppointmentID);
                anamnesisView.ShowDialog();
            }

               
        }

        public void ShowErrorMessageBox()
        {
            Utility.ShowWarning("Pacijent nema preglede u narednih 15 minuta.");
        }
    }
}
