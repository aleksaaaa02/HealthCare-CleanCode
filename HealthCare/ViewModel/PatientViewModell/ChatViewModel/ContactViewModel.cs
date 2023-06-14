using HealthCare.Application;
using HealthCare.Service;
using HealthCare.Service.UserService;
using HealthCare.ViewModel.PatientViewModell.ChatViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace HealthCare.Model
{
    public class ContactViewModel :  INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MessageService messageService => Injector.GetService<MessageService>();

        public DoctorService doctorService => Injector.GetService<DoctorService>();

        public NurseService nurseService => Injector.GetService<NurseService>();

        

        public ContactViewModel(Contact contactInput)
        {
            contact = contactInput;
        }

        private Contact _contact;
        public Contact contact
        {
            get
            {
                return _contact;
            }
            set
            {
                _contact = value;
                LoadMessages();
                RecalculateAll();
                RecalculateOtherUsername();
            }
        }



        private string _otherUsername;
        public string OtherUsername
        {
            get
            {
                return _otherUsername;
            }
            set
            {
                    string otherJMBG = _contact.Participants.Where(x => x != Context.Current.JMBG).First();
                    User otherUser = doctorService.TryGet(otherJMBG);
                    if (otherUser == null)
                    {
                        otherUser = nurseService.TryGet(otherJMBG);
                    }
                    _otherUsername = otherUser.Username;
                    DoctorColorBrush = CalculateDoctorColor();
                    OnPropertyChanged(nameof(OtherUsername));
            }
        }

        private ObservableCollection<MessageViewModel> _messages;
        public ObservableCollection<MessageViewModel> Messages
        {
            get { 
                return _messages; 
            }
            set
            {
                if (_messages != value)
                {              
                    _messages = value;
                    OnPropertyChanged(nameof(Messages));
                }
            }
        }

        private string lastMessage;
        public string LastMessage { 
            get
            { 
                return lastMessage; 
            } 
            set {
                lastMessage = value;
                OnPropertyChanged(nameof(LastMessage));
            } 
        }

        private SolidColorBrush _doctorColorBrush;
        public SolidColorBrush DoctorColorBrush
        {
            get { return _doctorColorBrush; }
            set
            {
                _doctorColorBrush = value;
                OnPropertyChanged(nameof(DoctorColorBrush));
            }
        }




        private int _unreadMessages;

        public int UnreadMessages
        {
            get => _unreadMessages;
            set
            {
                if (_unreadMessages != value)
                {
                    _unreadMessages = value;
                    OnPropertyChanged(nameof(UnreadMessages));
                }
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RecalculateOtherUsername()
        {
            OtherUsername = CalculateOtherUsername();
        }
        private string CalculateOtherUsername()
        {
            string otherJMBG = _contact.Participants.Where(x => x != Context.Current.JMBG).First();
            User otherUser = GetOtherUser();
            string otherUsername = otherUser.Username;
            return otherUsername;
        }

        public User GetOtherUser()
        {
            string otherJMBG = _contact.Participants.Where(x => x != Context.Current.JMBG).First();
            User otherUser = doctorService.TryGet(otherJMBG);
            if (otherUser == null)
            {
                otherUser = nurseService.TryGet(otherJMBG);
            }
            return otherUser;
        }

        public void RecalculateAll()
        {
            UnreadMessages = CalculateUnreadMessages();
            LastMessage = CalculateLastMassage();
        }

        public int CalculateUnreadMessages()
        {
            return Messages.Count(m => !m._Message.Seen && m._Message.SenderJMBG != Context.Current.JMBG);
        }

        private string CalculateLastMassage()
        {
            return Messages.LastOrDefault()?.MessageText;
        }


        private SolidColorBrush CalculateDoctorColor()
        {

            User otherUser = GetOtherUser();
            string userColor = otherUser.Color;
            
            Color color = (Color)ColorConverter.ConvertFromString(userColor);
            SolidColorBrush brush = new SolidColorBrush(color);
            return brush;
        }

        private void LoadMessages()
        {
            List<MessageViewModel> messages = new List<MessageViewModel>();
            int counter = 0;
            foreach (Message message in messageService.GetByContact(contact.ID))
            {
                MessageViewModel messageViewModel = new MessageViewModel(message);
                if(counter==0)
                {
                    messageViewModel.IsFirst = true;
                }
                else 
                { 
                    if(isSameMinute(messageViewModel._Message.Time, messages[counter-1]._Message.Time) && messages[counter-1]._Message.SenderJMBG==messageViewModel._Message.SenderJMBG)
                    {
                        messageViewModel.IsFirst = false;
                    }
                    else
                    {
                        messageViewModel.IsFirst = true;
                    }
                }

                messages.Add(messageViewModel);
                counter++;
            }
            Messages = new ObservableCollection<MessageViewModel>(messages);
        }


        public bool isSameMinute(DateTime message1Timestamp, DateTime message2Timestamp)
        {
            
            return message1Timestamp.Year == message2Timestamp.Year &&
                  message1Timestamp.Month == message2Timestamp.Month &&
                  message1Timestamp.Day == message2Timestamp.Day &&
                  message1Timestamp.Hour == message2Timestamp.Hour &&
                  message1Timestamp.Minute == message2Timestamp.Minute;

        }

    }


    
}
