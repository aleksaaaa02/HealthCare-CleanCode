using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using HealthCare.Model;

namespace HealthCare.View.PatientView
{
    /// <summary>
    /// Interaction logic for NotificationControl.xaml
    /// </summary>
    public partial class NotificationControl : UserControl
    {
        private readonly UserNotification _userNotification;

        public NotificationControl(UserNotification userNotification)
        {
            InitializeComponent();
            _userNotification = userNotification;
            NotificationCaption.Text = userNotification.caption;
            NotificationText.Text = userNotification.text;
            DateTime currentDate = DateTime.Now;
            TimeSpan timeDifference = userNotification.receiveTime - currentDate;
            int hoursDifference = (int)timeDifference.TotalHours;
            NotificationTime.Text = hoursDifference.ToString() + " sati";

            if (userNotification.isCustom)
            {
                SolidColorBrush brush = new SolidColorBrush(Colors.IndianRed);
                Header.Background = brush;
            }
        }

        private void btnNotifications_Click(object sender, RoutedEventArgs e)
        {
            if (expandSection.Visibility == Visibility.Collapsed)
            {
                NotificationTime.Text = "Vreme: " + _userNotification.receiveTime.ToString();
                expandSection.Visibility = Visibility.Visible;
                expandSection.Height = double.NaN;
                expandIcon.Icon = FontAwesome.Sharp.IconChar.Minus;
            }
            else
            {
                DateTime currentDate = DateTime.Now;
                TimeSpan timeDifference = _userNotification.receiveTime - currentDate;
                int hoursDifference = (int)timeDifference.TotalHours;
                NotificationTime.Text = hoursDifference.ToString() + " sati";
                expandSection.Visibility = Visibility.Collapsed;
                expandSection.Height = 0;
                expandIcon.Icon = FontAwesome.Sharp.IconChar.Plus;
            }
        }
    }
}