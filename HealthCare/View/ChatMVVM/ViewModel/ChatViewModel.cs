using HealthCare.Command;
using HealthCare.View.ChatMVVM.Model;
using HealthCare.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.View.ChatMVVM.ViewModel
{
    class ChatViewModel : ViewModelBase
    {
        public ObservableCollection<MessageModel> Messages { get; set; }

        public ObservableCollection<ContactModel> Contacts { get; set; }

        public RelayCommand SendCommand { get; set; }

        private ContactModel _selectedContact;

        public ContactModel SelectedContact
        {
            get { return _selectedContact; }
            set { 
                _selectedContact = value;
                OnPropertyChanged();
            }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { 
                _message = value;
                OnPropertyChanged();
            }
        }

        public ChatViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();
            SendCommand = new RelayCommand(o =>
            {
                Messages.Add(new MessageModel
                {
                    Message = Message,
                    FirstMessage = false
                }); ;

                Message = "";

            });

            Messages.Add(new MessageModel
            {
                Username = "Tvoja keva",
                UsernameColor = "#409aff",
                ImageSource = "nesto",
                Message = "Test",
                Time = DateTime.Now,
                IsNativeOrigin = false,
                FirstMessage = true
        });
            for(int i = 0; i < 3; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "Chanel",
                    UsernameColor = "#409aff",
                    ImageSource = "https://images.genius.com/0ee67ebc42b7c0d9924dcd8148dea503.1000x1000x1.png",
                    Message = "Test",
                    Time = DateTime.Now,
                    IsNativeOrigin = false,
                    FirstMessage = false
                });
            }

            for (int i = 0; i < 3; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "Mitar",
                    UsernameColor = "#409aff",
                    ImageSource = "https://maiaapi.monics.me/uploads/Mitar_Perovic_ed691877c4.jpeg",
                    Message = "Responsive ti je mama",
                    Time = DateTime.Now,
                    IsNativeOrigin = true,
                });
            }

            Messages.Add(new MessageModel
            {
                Username = "Mitar ali sussy",
                UsernameColor = "#409aff",
                ImageSource = "https://maiaapi.monics.me/uploads/Mitar_Perovic_ed691877c4.jpeg",
                Message = "Sex?",
                Time = DateTime.Now,
                IsNativeOrigin = true,
            });

            Contacts.Add(new ContactModel
            {
                Username = "Chanel",
                ImageSource = "https://images.genius.com/0ee67ebc42b7c0d9924dcd8148dea503.1000x1000x1.png",
                Messages = Messages
            }) ;

            Contacts.Add(new ContactModel
            {
                Username = "Mitar",
                ImageSource = "https://maiaapi.monics.me/uploads/Mitar_Perovic_ed691877c4.jpeg",
                Messages = Messages
            });

            Contacts.Add(new ContactModel
            {
                Username = "Mitar ali sussy",
                ImageSource = "https://maiaapi.monics.me/uploads/Mitar_Perovic_ed691877c4.jpeg",
                Messages = Messages
            });

        }
    }
}
