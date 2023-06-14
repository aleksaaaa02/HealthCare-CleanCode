using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.Service.UserService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HealthCare.ViewModel.PatientViewModell.ChatViewModel
{
    public class MessageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public DoctorService doctorService => Injector.GetService<DoctorService>();

        public NurseService nurseService => Injector.GetService<NurseService>();

        private Message message;

        public Message _Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                OnPropertyChanged(nameof(_Message));
                recalculateAll();
            }
        }

        private bool isFirst;

        public bool IsFirst
        {
            get
            {
                return isFirst;
            }
            set
            {
                isFirst = value;
                OnPropertyChanged(nameof(IsFirst));
            }
        }

        private SolidColorBrush senderColor;

        public SolidColorBrush SenderColor
        {
            get
            {
                return senderColor;
            }
            set
            {
                senderColor = value;
                OnPropertyChanged(nameof(SenderColor));
            }
        }

        private string time;

        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        private string senderName;
        public string SenderName
        {
            get
            {
                return senderName;
            }
            set
            {
                senderName = value;
                OnPropertyChanged(nameof(SenderName));
            }
        }

        private string messageText;

        public string MessageText
        {
            get
            {
                return messageText;
            }
            set
            {
                messageText = value;
                OnPropertyChanged(nameof(MessageText));
            }
        }

        public MessageViewModel(Message messageInput)
        {
            _Message = messageInput;
            
        }

        public void recalculateAll()
        {
            MessageText = message.MessageText;
            Time = message.Time.ToString();

            User senderUser = doctorService.TryGet(message.SenderJMBG);
            if (senderUser == null)
            {
                senderUser = nurseService.TryGet(message.SenderJMBG);
            }

            SenderName = senderUser.Username;




            User userSender = doctorService.TryGet(message.SenderJMBG);
            if (userSender == null)
            {
                userSender = nurseService.TryGet(message.SenderJMBG);
            }

            string userColor = userSender.Color;

            Color color = (Color)ColorConverter.ConvertFromString(userColor);
            SolidColorBrush brush = new SolidColorBrush(color);
            SenderColor = brush;

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}