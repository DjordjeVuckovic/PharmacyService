// File:    RoomRepository.cs
// Author:  nsred
// Created: Thursday, April 7, 2022 6:14:41 PM
// Purpose: Definition of Class RoomRepository

using System;
using System.Collections.Generic;
using CaduceusHealth.Model;

namespace CaduceusHealth.Repository
{
   public class RoomRepository
   {
      private List<Room> rooms;
      
      public Room GetById(string roomId)
      {
         throw new NotImplementedException();
      }
      
      public List<Room> GetAll()
      {
         throw new NotImplementedException();
      }
      
      public bool Create(Room room)
      {
         throw new NotImplementedException();
      }
      
      public bool Update(Room room)
      {
         throw new NotImplementedException();
      }
      
      public bool Delete(string roomId)
      {
         throw new NotImplementedException();
      }
      
      public FileHandler fileHandler;
   
   }
}