// File:    Address.cs
// Author:  djord
// Created: Monday, March 28, 2022 9:00:05 AM
// Purpose: Definition of Class Address

using System;

namespace CaduceusHealth.Model
{
   public class Address
   {
      public string City { get; set; }
      public string Number { get; set; }
      public string Country { get; set; }
      public string Street { get; set; }
      public int Postcode { get; set; }
   
   }
}