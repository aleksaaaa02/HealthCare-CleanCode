﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.View.ChatMVVM.Model
{
    class ContactModel
    {
        public String Username { get; set; }
        public String ImageSource { get; set; }

        public  ObservableCollection<MessageModel> Messages { get; set; }

        public String LastMessage => Messages.Last().Message;
    }
}
