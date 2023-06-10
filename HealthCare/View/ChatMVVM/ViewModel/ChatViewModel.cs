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

        public ObservableCollection<ContactViewModel> Contacts { get; set; }

        public readonly DoctorService doctorService;

        public readonly ContactService contactService;

        public readonly MessageService messageService;
        public RelayCommand SendCommand { get; set; }

        public event EventHandler ScrollToBottom;

        private ContactViewModel _selectedContact;

        public ContactViewModel SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                foreach(Message message in _selectedContact.Messages.Where(x => x.SenderJMBG!=Context.Current.JMBG))
                {
                    message.Seen = true;
                    messageService.Update(message);
                }
                _selectedContact.RecalculateAll();
                OnPropertyChanged(nameof(SelectedContact));
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
            Contacts = new ObservableCollection<ContactViewModel>();
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
                        contactID = _selectedContact.contact.ID,
                        MessageText = Message,
                        SenderJMBG = Context.Current.JMBG,
                        Time = DateTime.Now,
                        SenderName = doctorService.Get(Context.Current.JMBG).Username,
                        Seen = false


                };
                    _selectedContact.Messages.Add(message);
                    messageService.Add(message);
                }
                Message = "";

            });
        }
        public void loadContacts()
        {
            List<Contact> contacts = contactService.GetForUser(Context.Current.JMBG);
            List<ContactViewModel> contactViewModels = new List<ContactViewModel>();
            foreach (Contact contact in contacts)
            {
                ContactViewModel a = new ContactViewModel(contact);
                contactViewModels.Add(a);               
            }
            Contacts = new ObservableCollection<ContactViewModel>(contactViewModels);
        }
    }
}
