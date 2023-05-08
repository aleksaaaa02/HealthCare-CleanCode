﻿using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.View.DoctorView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCare.ViewModel.DoctorViewModel.RoomReservation.Commands
{

    public class ReservateRoomCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private readonly RoomReservationViewModel _roomReservationViewModel;
        private readonly Window _window;
        private Appointment _appointment;

        public ReservateRoomCommand(Hospital hospital, Window window, RoomReservationViewModel viewModel, Appointment appointment) { 
            _hospital = hospital;
            _roomReservationViewModel = viewModel;
            _window = window;
            _appointment = appointment;
        }   

        public override void Execute(object parameter)
        {

            if (_roomReservationViewModel.SelectedRoom == null) return;
          
            
            int roomId = _roomReservationViewModel.SelectedRoom.RoomId;
            
            _window.Close();

            StartExamination(roomId);

        }
        private void StartExamination(int roomId)
        {
            new DoctorExamView(_hospital, _appointment, roomId).Show();
        }
    }
}
