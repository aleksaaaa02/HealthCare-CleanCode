﻿using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class OrderItemViewModel : ViewModelBase
    {
        private readonly Equipment _equipment;
        public bool IsSelected { get; set; }
        public string EquipmentName => _equipment.Name;
        public string EquipmentType => _equipment.TranslateType();
        public int CurrentQuantity { get; }
        public string OrderQuantity { get; set; }

        public OrderItemViewModel(Equipment equipment, int currentQuantity)
        {
            _equipment = equipment;
            IsSelected = false;
            CurrentQuantity = currentQuantity;
            OrderQuantity = "0";
        }
    }
}
