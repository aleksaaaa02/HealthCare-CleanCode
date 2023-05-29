﻿using HealthCare.Application;
using HealthCare.ViewModel.ManagerViewModel;
using System;
using System.Windows;

namespace HealthCare.View.ManagerView
{
    public partial class InventoryListingView : Window
    {
        private InventoryListingViewModel _model;

        public InventoryListingView()
        {
            InitializeComponent();

            _model = new InventoryListingViewModel();
            DataContext = _model;
        }

        private void Button_Reset(object sender, RoutedEventArgs e)
        {
            _model.LoadAll();
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
