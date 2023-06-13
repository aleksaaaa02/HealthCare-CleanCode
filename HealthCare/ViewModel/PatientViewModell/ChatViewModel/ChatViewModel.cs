using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View.PatientView;
using HealthCare.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace HealthCare.ViewModel.PatientViewModell.ChatViewModel
{
    public class ChatViewModel : ViewModelBase
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

        private ObservableCollection<ContactViewModel> contacts;


        public ObservableCollection<ContactViewModel> Contacts
        {
            get { return contacts; }
            set { contacts = value;
                OnPropertyChanged(nameof(Contacts));
            }
        }



        public readonly DoctorService doctorService;

        private User loggedUser;
        public User LoggedUser
        {
            get => loggedUser;
            set
            {
                { 
                    loggedUser = value; OnPropertyChanged();
                    LoggedColorBrush = CalculateLoggedColor();

                }
            }
        }

        private SolidColorBrush loggedColorBrush;
        public SolidColorBrush LoggedColorBrush
        {
            get { return loggedColorBrush; }
            set
            {
                loggedColorBrush = value;
                OnPropertyChanged(nameof(LoggedColorBrush));
                
            }
        }



        public ContactService contactService;

        public readonly MessageService messageService;

        public readonly NurseService nurseService;
        public RelayCommand SendCommand { get; set; }
        public RelayCommand OpenAddContactWindow { get; set; }

        public event EventHandler ScrollToBottom;

        private ContactViewModel _selectedContact;

        public ContactViewModel SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                foreach (MessageViewModel message in _selectedContact.Messages.Where(x => x._Message.SenderJMBG != Context.Current.JMBG))
                {
                    message._Message.Seen = true;
                    messageService.Update(message._Message);
                }
                _selectedContact.RecalculateAll();
                OtherUsername = _selectedContact.OtherUsername;
                OnPropertyChanged(nameof(SelectedContact));
            }
        }

        private string otherUsername;

        public string OtherUsername
        {
            get { return otherUsername; }
            set { otherUsername = value;
                OnPropertyChanged();
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
            nurseService = Injector.GetService<NurseService>();
            LoggedUser = Context.Current;
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
                        Seen = false


                    };
                    _selectedContact.Messages.Add(new MessageViewModel(message));
                    messageService.Add(message);
                }
                Message = "";

            });

            OpenAddContactWindow = new RelayCommand(o =>
            {
                AddContactView addContactWindow = new AddContactView(this);
                addContactWindow.ShowDialog();
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

        private SolidColorBrush CalculateLoggedColor()
        {
            string loggedColor = Context.Current.Color ?? "#FF0000";
            Color color = (Color)ColorConverter.ConvertFromString(loggedColor);
            SolidColorBrush brush = new SolidColorBrush(color);
            return brush;
        }
    }
}
