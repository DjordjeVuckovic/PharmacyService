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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TechHealth.Annotations;
using TechHealth.Controller;
using TechHealth.Conversions;
using TechHealth.Core;
using TechHealth.Model;
using TechHealth.Repository;
using TechHealth.View.ManagerView.CRUDRooms;

namespace TechHealth.View.ManagerView.VieW
{
    /// <summary>
    /// Interaction logic for RoomView.xaml
    /// </summary>
    public partial class RoomView : UserControl, INotifyPropertyChanged
    {
        private ObservableCollection<Room> rooms;

        private RoomController roomController = new RoomController();
        private RoomMergingController roomMergingController = new RoomMergingController();
        private RoomSeparationController roomSeparationController = new RoomSeparationController();
        private Room selectedItem;
        public event PropertyChangedEventHandler PropertyChanged;
        public static Timer timer;
        public static Timer timer1;
        public RelayCommand AddRoomCommand { get; set; }
        public RelayCommand DeleteRoomCommand { get; set; }
        public RelayCommand UpdateRoomCommand { get; set; }
        public RelayCommand InventoryCommand { get; set; }
        public RelayCommand RenovationCommand { get; set; }
        public RelayCommand MergeRoomsCommand { get; set; }
        public RelayCommand SeparateRoomsCommand { get; set; }
        public Room SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Room> Rooms
        {
            get
            {
                return rooms;
            }

            set
            {
                rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }
        public RoomView()
        {
            timer = new Timer(new TimerCallback(roomMergingController.MergeOnDate), null, 1000, 60000);
            //timer1 = new Timer(new TimerCallback(roomSeparationController.SeparateOnDate), null, 1000, 60000);
            InitializeComponent();
            DataContext = this;
            rooms = new ObservableCollection<Room>(roomController.GetAll());
            AddRoomCommand = new RelayCommand(param => ExecuteAdd(), param => CanExecuteAdd());
            DeleteRoomCommand = new RelayCommand(param => ExecuteDel(), param => CanExecuteDel());
            UpdateRoomCommand = new RelayCommand(param => ExecuteUpdate(), param => CanExecuteUpdate());
            InventoryCommand = new RelayCommand(param => ExecuteInventory(), param => CanExecuteInventory());
            RenovationCommand = new RelayCommand(param => ExecuteRenovation(), param => CanExecuteRenovation());
            MergeRoomsCommand = new RelayCommand(param => ExecuteMerge(), param => CanExecuteMerge());
            SeparateRoomsCommand = new RelayCommand(param => ExecuteSeparation(), param => CanExecuteSeparation());
        }

        private bool CanExecuteSeparation()
        {
            if (selectedItem == null)
            {
                return false;
            }

            return true;
        }

        private void ExecuteSeparation()
        {
            new SeparateRooms(selectedItem).ShowDialog();
        }

        private bool CanExecuteMerge()
        {
            if (selectedItem == null)
            {
                return false;
            }

            return true;
        }

        private void ExecuteMerge()
        {
            new MergeRooms(selectedItem).ShowDialog();
        }

        private bool CanExecuteRenovation()
        {
            if (selectedItem == null)
            {
                return false;
            }

            return true;
        }

        private void ExecuteRenovation()
        {
            new AddRenovation(selectedItem).ShowDialog();
        }

        private bool CanExecuteInventory()
        {
            if (selectedItem == null)
            {
                return false;
            }

            return true;
        }

        private void ExecuteInventory()
        {
            new RoomInventory(selectedItem).ShowDialog();
        }

        private bool CanExecuteUpdate()
        {
            if (selectedItem == null)
            {
                return false;
            }

            return true;
        }

        private void ExecuteUpdate()
        {
            new UpdateForm(selectedItem).ShowDialog();
        }

        private bool CanExecuteDel()
        {
            if (selectedItem == null)
            {
                return false;
            }

            return true;
        }

        private void ExecuteDel()
        {
            roomController.Delete(selectedItem.roomId);
            rooms.Remove(selectedItem);
            MessageBox.Show("You have successfully deleted the room");
        }

        private bool CanExecuteAdd()
        {
            return true;
        }

        private void ExecuteAdd()
        {
            new AddForm(rooms).ShowDialog();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
