using HealthCare.Context;
using HealthCare.Model;
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
    /// Interaction logic for PatientNotificationsView.xaml
    /// </summary>
    public partial class PatientNotificationsView : UserControl
    {
        List<UserNotification> notifications;
        public PatientNotificationsView(Hospital hospital)
        {
            InitializeComponent();
            //DataContext = new PatientNotificationsViewModel(hospital);
            notifications = hospital.UserNotificationService.GetForUser(hospital.Current.JMBG);

            foreach (UserNotification userNotification in notifications)
            {
                NotificationControl notificationControl = new NotificationControl();
                notificationControl.NotificationCaption.Text = userNotification.caption;
                notificationControl.NotificationText.Text = userNotification.text;
                //"#136c63
                if (userNotification.isCustom) {
                    Color customColor = (Color)ColorConverter.ConvertFromString("#963BC4");
                    SolidColorBrush customBrush = new SolidColorBrush(customColor);

                    notificationControl.Header.Background = customBrush;
                }
                NotificationsPanel.LastChildFill = true;

                // Add the control as the last child of the DockPanel
                DockPanel.SetDock(notificationControl, Dock.Top);
                NotificationsPanel.Children.Add(notificationControl);
            }
        }
    }
}
