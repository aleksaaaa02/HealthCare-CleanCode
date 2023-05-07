using HealthCare.Context;
using HealthCare.View.ReceptionView;
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
    /// <summary>
    /// Interaction logic for NurseMenu.xaml
    /// </summary>
    public partial class NurseMenu : Window
    {
        private readonly Hospital hospital;
        private Window window;
        public NurseMenu(Window window,Hospital hospital)
        {
            this.window = window;
            this.hospital = hospital;
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            window.Show();
            Close();
        }

        private void mnuCRUD_Click(object sender, RoutedEventArgs e)
        {
            new NurseMainView(hospital).ShowDialog();
        }

        private void mnuReception_Click(object sender, RoutedEventArgs e)
        {
            new MainReceptionView(hospital).ShowDialog();
        }
    }
}
