﻿using System.Windows.Controls;
using System.Windows.Input;
using HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel;

namespace HealthCare.View.ManagerView.AnalyticsView
{
    public partial class SurveyListingControl : UserControl
    {
        private readonly SurveyListingViewModel _model;

        public SurveyListingControl(SurveyListingViewModel model)
        {
            InitializeComponent();

            _model = model;
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _model.ShowSurveyCommentsCommand.Execute(this);
        }
    }
}