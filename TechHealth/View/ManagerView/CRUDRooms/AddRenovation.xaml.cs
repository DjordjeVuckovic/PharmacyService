﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TechHealth.Core;
using TechHealth.Model;
using TechHealth.Repository;

namespace TechHealth.View.ManagerView.CRUDRooms
{
    /// <summary>
    /// Interaction logic for AddRenovation.xaml
    /// </summary>
    public partial class AddRenovation : Window
    {
        private List<String> rooms;
        private Room selected;
        private RoomRenovation rr;
        public RelayCommand CancelRenovationCommand { get; set; }
        public AddRenovation(Room rm)
        {
            InitializeComponent();
            DataContext = this;
            selected = rm;
            rooms = RoomRepository.Instance.GetRoomIDs();
            TxtRoomID.Text = selected.roomId;
            CancelRenovationCommand = new RelayCommand(param => ExeceuteCancel(), param =>CanExecuteCancel());

            rr = RoomRenovationRepository.Instance.GetRrByRoomID(selected.roomId);
            RStart.SelectedDate = rr.RenovationStart;
            REnd.SelectedDate = rr.RenovationEnd;
        }

        private bool CanExecuteCancel()
        {
            return !RoomRenovationRepository.Instance.ExistsInRenovations(selected.roomId);     
        }

        private void ExeceuteCancel()
        {
            RoomRenovationRepository.Instance.Delete(rr.RenovationID);
            this.Close();
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            RoomRenovation r = new RoomRenovation();
            r.RoomID = selected.roomId;
            //string date = DpDateTime.Text;
            //string dateTime = date + " " + TxtTime.Text;
            //dto.ReallocationTime = DateTime.Parse(dateTime);
            string dateStart = RStart.Text;
            string dateStartTime = dateStart + " " + TxtStartTime.Text;
            r.RenovationStart = DateTime.Parse(dateStartTime);
            //r.RenovationStart = (DateTime)RStart.SelectedDate;
            string dateEnd = REnd.Text;
            string dateEndTime = dateEnd + " " + TxtEndTime.Text;
            r.RenovationEnd = DateTime.Parse(dateEndTime);
            //r.RenovationEnd = (DateTime)REnd.SelectedDate;
            r.RenovationID = Guid.NewGuid().ToString("N");

            if (RoomRenovationRepository.Instance.ExistsInRenovations(selected.roomId))     //da li je vec zakazano renoviranje te sobe
            {
                if (AppointmentRepository.Instance.CanSetRenovation(r.RenovationStart, r.RenovationEnd, r.RoomID))  //da li ima app zakazan u tom periodu
                {
                    if (!EquipmentReallocationRepository.Instance.IsReallocationHappening(r.RenovationStart, r.RenovationEnd, r.RoomID))
                    {
                        RoomRenovationRepository.Instance.Create(r);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Reallocation is happening in that period!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("There is an appointment scheduled in that period!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("This room already has scheduled renovation!");
                return;
            }
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
