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
using TechHealth.Controller;
using TechHealth.DTO;
using TechHealth.Model;
using TechHealth.Repository;

namespace TechHealth.View.ManagerView.CRUDRooms
{
    /// <summary>
    /// Interaction logic for ReallocateForm.xaml
    /// </summary>
    public partial class ReallocateForm : Window
    {
        //private List<Equipment> eqList;
        private List<String> rooms;
        private List<String> dstRooms;
        private Equipment selected;
        private List<Room> eqs;
        private EquipmentReallocationController reallocationController = new EquipmentReallocationController();
        private EquipmentController equipmentController = new EquipmentController();
        private RoomController roomController = new RoomController();
        public ReallocateForm(Equipment eq)
        {
            InitializeComponent();
            DataContext = this;
            selected = EquipmentRepository.Instance.GetById(eq.id);
            rooms = RoomRepository.Instance.GetRoomNames();
            //eqList = EquipmentRepository.Instance.GetAllToList();
            //eqs = RoomRepository.Instance.GetRoomsByEq(selected.name);
            //dstRooms = RoomRepository.Instance.GetRoomNames(eqs);
            CbSourceRoom.ItemsSource = rooms;
            CbDestinationRoom.ItemsSource = rooms;
            TxtEqId.Text = selected.name;
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            EquipmentReallocationDTO dto = new EquipmentReallocationDTO();
            dto.EquipmentName = TxtEqId.Text;
            dto.SourceRoomID = CbSourceRoom.Text;
            dto.DestinationRoomID = CbDestinationRoom.Text;
            dto.AmountMoving = Int32.Parse(TxtAmount.Text);
            string date = DpDateTime.Text;
            string dateTime = date + " " + TxtTime.Text;
            dto.ReallocationTime = DateTime.Parse(dateTime);
            //dto.ReallocationTime = DpDateTime.SelectedDate;
            dto.ReallocationID = Guid.NewGuid().ToString("N");

            RoomEquipment reDst = new RoomEquipment();
            RoomEquipment reSrc = new RoomEquipment();
            reDst = RoomEquipmentRepository.Instance.GetReByKey(dto.EquipmentName, dto.DestinationRoomID);
            reSrc = RoomEquipmentRepository.Instance.GetReByKey(dto.EquipmentName, dto.SourceRoomID);

            if (!(reSrc.Quantity >= dto.AmountMoving))
            {
                MessageBox.Show("Can't trasnfer that much!");
                return;
            }
            if (dto.SourceRoomID != dto.DestinationRoomID)
            {
                if (AppointmentRepository.Instance.CanDoReallocation(dto.ReallocationTime, dto.SourceRoomID, dto.DestinationRoomID))
                {
                    if (RoomRenovationRepository.Instance.IsValidDate(dto.ReallocationTime, dto.SourceRoomID, dto.DestinationRoomID))
                    {
                        if (DateTime.Compare(DateTime.Now, dto.ReallocationTime) == 0)
                        {
                            reallocationController.SubmitReallocation(dto);
                        }
                        else
                        {
                            EquipmentReallocationRepository.Instance.Create(dto);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid date!");
                    }
                }
                else
                {
                    MessageBox.Show("There is an appointment scheduled on that date!");
                }
            }
            else
            {
                MessageBox.Show("Invalid rooms for transfer selected!");
                return;
            }
            this.Close();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
