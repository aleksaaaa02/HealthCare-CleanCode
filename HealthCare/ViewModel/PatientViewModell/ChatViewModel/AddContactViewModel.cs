using HealthCare.Application;
using HealthCare.Application.Common;
using HealthCare.Command;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCare.ViewModel.PatientViewModell.ChatViewModel
{
    public class AddContactViewModel
    {
        private readonly NurseService nurseService;
        private readonly DoctorService doctorService;
        private readonly ContactService contactService;
        public RelayCommand AddContactCommand { get; set; }


        private ObservableCollection<User> allUsers;

        public ObservableCollection<User> AllUsers
        {
            get { return allUsers; }
            set { allUsers = value; }
        }

        private User selectedUser;

        public User SelectedUser
        {
            get { return selectedUser; }
            set { selectedUser = value; }
        }


        public AddContactViewModel(ChatViewModel previousModel)
        {
            nurseService = Injector.GetService<NurseService>();
            doctorService = Injector.GetService<DoctorService>();
            contactService = Injector.GetService<ContactService>();
            fillAllUsers();


            AddContactCommand = new RelayCommand(o =>
            {
                if (selectedUser != null)
                {
                    if(selectedUser.JMBG==Context.Current.JMBG)
                    {
                        ViewUtil.ShowError("Ne mozete dodati sebe");
                        return;
                    }
                    int count = contactService.GetForUser(Context.Current.JMBG).Count(contact => contact.Participants.Contains(Context.Current.JMBG) && contact.Participants.Contains(selectedUser.JMBG));

                    if (count > 0)
                    {
                        ViewUtil.ShowError("Vec imate tog kontakta");
                    }
                    else
                    {
                        contactService.Add(
                            new Contact
                            {
                                Participants = new List<String> { Context.Current.JMBG, SelectedUser.JMBG }
                            }
                            );
                        previousModel.loadContacts();
                        ViewUtil.ShowInformation("Uspesno ste dodali kontakt");
                        

                    }
                }
                else
                {
                    ViewUtil.ShowError("Morate izabrati korisnika");
                }
            });
        }

        public void fillAllUsers()
        {
            List<User> AllUsersList = new List<User>();
            foreach (var item in nurseService.GetAll())
            {
                AllUsersList.Add(item);
            }
            foreach (var item in doctorService.GetAll())
            {
                AllUsersList.Add(item);
            }
            AllUsers = new ObservableCollection<User>(AllUsersList);
        }
    }
}
