﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TechHealth.Annotations;
using TechHealth.Controller;
using TechHealth.Model;
using TechHealth.Repository;

namespace TechHealth.View.ManagerView.CRUDRooms
{
    /// <summary>
    /// Interaction logic for RoomInventory.xaml
    /// </summary>
    public partial class RoomInventory : Window, INotifyPropertyChanged
    {
        private ObservableCollection<RoomEquipment> reList;
        public event PropertyChangedEventHandler PropertyChanged;
        public static Timer timer;
        private EquipmentReallocationController eqReallocationController = new EquipmentReallocationController();

        public ObservableCollection<RoomEquipment> ReList
        {
            get
            {
                return reList;
            }

            set
            {
                reList = value;
                OnPropertyChanged(nameof(ReList));
            }
        }
        public RoomInventory(Room room)
        {
            timer = new Timer(new TimerCallback(eqReallocationController.ReallocateOnDate), null, 1000, 60000);
            InitializeComponent();
            DataContext = this;
            reList = new ObservableCollection<RoomEquipment>(RoomEquipmentRepository.Instance.GetRoomEqListByRoomID(room.roomId));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
