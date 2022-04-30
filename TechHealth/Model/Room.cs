// File:    Room.cs
// Author:  djord
// Created: Monday, March 28, 2022 8:47:46 AM
// Purpose: Definition of Class Room

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PostSharp.Patterns.Model;

namespace TechHealth.Model
{
    
    public class Room
    {
        
        public RoomTypes roomTypes { get; set; }
        public string roomId { get; set; }
        public int floor { get; set; }
        public bool availability { get; set; }
        public List<Equipment> equipment { get; set; }
        [JsonIgnore]
        public bool ShouldSerialize { get; set; }
        public Room() { }
        public Room(RoomTypes rt, string id, int flr, bool available, List<Equipment> list)
        {
            roomTypes = rt;
            roomId = id;
            floor = flr;
            availability = available;
            equipment = list;
            ShouldSerialize = true;
        }

        public void Add(Equipment eq)
        {
            equipment.Add(eq);
        }
        public bool ShouldSerializeroomTypes()
        {
            return ShouldSerialize;
        }
        public bool ShouldSerializefloor()
        {
            return ShouldSerialize;
        }
        public bool ShouldSerializeavailability()
        {
            return ShouldSerialize;
        }
        public bool ShouldSerializeequipment()
        {
            return ShouldSerialize;
        }

       
    }
}