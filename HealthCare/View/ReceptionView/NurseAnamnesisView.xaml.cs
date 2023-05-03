using HealthCare.Context;
using System;
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

namespace HealthCare.View.PatientView
{
    public partial class NurseAnamnesisView : Window
    {
        private readonly Hospital hospital = new Hospital();
        private readonly int appointmentID;
        public NurseAnamnesisView(Hospital hospital,int appointmentID)
        {
            InitializeComponent();
            this.hospital = hospital;
            this.appointmentID = appointmentID;
        }
    }
}
