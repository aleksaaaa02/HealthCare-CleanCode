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
    /// Interaction logic for NotificationControl.xaml
    /// </summary>
    public partial class NotificationControl : UserControl
    {
        public NotificationControl()
        {
            InitializeComponent();
        }

        private void btnNotifications_Click(object sender, RoutedEventArgs e)
        {
            if (expandSection.Visibility == Visibility.Collapsed)
            {
                expandSection.Visibility = Visibility.Visible;
                expandSection.Height = double.NaN;
                expandIcon.Icon = FontAwesome.Sharp.IconChar.Minus;
            }
            else
            {
                expandSection.Visibility = Visibility.Collapsed;
                expandSection.Height = 0;
                expandIcon.Icon = FontAwesome.Sharp.IconChar.Plus;
            }
        }
    }
}
