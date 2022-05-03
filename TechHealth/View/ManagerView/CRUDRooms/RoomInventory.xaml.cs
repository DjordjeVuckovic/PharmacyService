﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TechHealth.Annotations;
using TechHealth.Model;
using TechHealth.Repository;

namespace TechHealth.View.ManagerView.CRUDRooms
{
    /// <summary>
    /// Interaction logic for RoomInventory.xaml
    /// </summary>
    public partial class RoomInventory : Window, INotifyPropertyChanged
    {
        //private ObservableCollection<Equipment> eqList;

        private ObservableCollection<RoomEquipment> reList;

        public event PropertyChangedEventHandler PropertyChanged;

        //public ObservableCollection<Equipment> EqList
        //{
        //    get 
        //    {
        //        return eqList;
        //    }

        //    set
        //    {
        //        eqList = value;
        //        OnPropertyChanged(nameof(EqList));
        //    }
        //}

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
            InitializeComponent();
            DataContext = this;
            //eqList = new ObservableCollection<Equipment>(EquipmentRepository.Instance.GetEqListByRoomID(room.roomId));
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
