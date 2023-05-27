using HealthCare.Context;
using HealthCare.View.AppointmentView;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HealthCare.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientMainWindow.xaml
    /// </summary>
    public partial class PatientMainWindow : Window
    {
        Hospital _hospital;
        MainWindow _mainWindow;
        public PatientMainWindow(MainWindow mainWindow,Hospital hospital)
        {
            InitializeComponent();
            _hospital = hospital;
            _mainWindow = mainWindow;
            //#179c8c green
            //#effcfa white
            btnCrud.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            btnCrud.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainContentGrid.Content = new AppointmentMainView(_hospital);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainContentGrid.Content = new PriorityAppointmentView(_hospital);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            mainContentGrid.Content = new PatientRecordView(_hospital);
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Close();
            _mainWindow.Show();
        }

        //private void Button_Click_4(object sender, RoutedEventArgs e)
        //{

        //    DoubleAnimation animation = new DoubleAnimation
        //    {
        //        To = 150, // Target size, in this case, half the original size
        //        Duration = TimeSpan.FromSeconds(0.5) // Animation duration
        //    };

        //    // Set the animation to modify the Width and Height properties of the dashboard container
        //    nzm.BeginAnimation(Grid.WidthProperty, animation);
        //}
    }
}
