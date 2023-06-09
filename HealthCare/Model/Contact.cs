using HealthCare.Application;
using HealthCare.Repository;
using HealthCare.Serialize;
using HealthCare.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace HealthCare.Model
{
    public class Contact : RepositoryItem, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override object Key
        {
            get => ID;
            set => ID = (int)value;
        }

        private int _id;
        public int ID
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }

        private string _otherUsername;
        public string OtherUsername
        {
            get => _otherUsername;
            set
            {
                if (_otherUsername != value)
                {
                    _otherUsername = value;
                    OnPropertyChanged(nameof(OtherUsername));
                }
            }
        }

        public List<string> Participants { get; set; }

        private ObservableCollection<Message> _messages;
        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set
            {
                if (_messages != value)
                {              
                    _messages = value;
                    OnPropertyChanged(nameof(Messages));
                    OnPropertyChanged(nameof(LastMessage));
                }
            }
        }

        private int _unreadMessages;

        public int UnreadMessages
        {
            get => _unreadMessages;
            private set
            {
                if (_unreadMessages != value)
                {
                    _unreadMessages = value;
                    OnPropertyChanged(nameof(UnreadMessages));
                }
            }
        }

        public MessageService messageService => Injector.GetService<MessageService>();

        public string? LastMessage => Messages.LastOrDefault()?.message;

        public override string[] Serialize()
        {
            string recipients = SerialUtil.ToString(Participants);
            return new string[] { ID.ToString(), recipients };
        }

        public override void Deserialize(string[] values)
        {
            ID = int.Parse(values[0]);
            Participants = values[1].Split("|").ToList();
            List<Message> messagesList = messageService.GetByContact(ID);
            Messages = new ObservableCollection<Message>(messagesList);
            OtherUsername = Participants.FirstOrDefault(item => item != Context.Current.JMBG);
            UnreadMessages = Messages.Count(m => !m.seen && m.senderJMBG != Context.Current.JMBG);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateUnreadMessages(int newUnreadMessages)
        {
            UnreadMessages = newUnreadMessages;
        }
    }
}
