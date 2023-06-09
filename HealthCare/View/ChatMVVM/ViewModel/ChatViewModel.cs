using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace HealthCare.View.ChatMVVM.ViewModel
{
    class ChatViewModel : ViewModelBase
    {
        private ObservableCollection<Message> _messages;

        public ObservableCollection<Message> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Contact> Contacts { get; set; }

        public readonly DoctorService doctorService;

        public readonly ContactService contactService;

        public readonly MessageService messageService;
        public RelayCommand SendCommand { get; set; }

        public event EventHandler ScrollToBottom;

        private Contact _selectedContact;

        public Contact SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                loadMessages();
                OnPropertyChanged();
                foreach(Message message in Messages.Where(x => x.senderJMBG!=Context.Current.JMBG))
                {
                    message.seen = true;
                    messageService.Update(message);
                }
                _selectedContact.UpdateUnreadMessages(Messages.Count(m => !m.seen && m.senderJMBG != Context.Current.JMBG));
            }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public ChatViewModel()
        {
            Messages = new ObservableCollection<Message>();
            Contacts = new ObservableCollection<Contact>();
            doctorService = Injector.GetService<DoctorService>();
            contactService = Injector.GetService<ContactService>();
            messageService = Injector.GetService<MessageService>();
            loadContacts();

            SendCommand = new RelayCommand(o =>
            {
                if (_selectedContact != null)
                {
                    Message message = new Message()
                    {
                        contactID = _selectedContact.ID,
                        message = Message,
                        senderJMBG = Context.Current.JMBG,
                        time = DateTime.Now,
                        senderName = doctorService.Get(Context.Current.JMBG).Username,
                        seen = false


                };
                    Messages.Add(message);
                    messageService.Add(message);
                }
                Message = "";

            });
        }

        public void loadMessages()
        {
            if (_selectedContact != null)
            {
                List<Message> messages = messageService.GetByContact(_selectedContact.ID);
                foreach (Message message in messages)
                {
                    message.senderName = doctorService.Get(_selectedContact.Participants.FirstOrDefault(item => item != Context.Current.JMBG)).Username;

                }
                Messages = new ObservableCollection<Message>(messages);
            }

            
        }
        public void loadContacts()
        {
            List<Contact> contacts = contactService.GetForUser(Context.Current.JMBG);
            foreach (Contact contact in contacts)
            {
                String otherJMBG = contact.Participants.FirstOrDefault(item => item != Context.Current.JMBG);
                contact.OtherUsername = doctorService.Get(otherJMBG).Username;
            }
            Contacts = new ObservableCollection<Contact>(contacts);
        }
    }
}
