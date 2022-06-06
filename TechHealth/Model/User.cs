// File:    User.cs
// Author:  djord
// Created: Saturday, April 2, 2022 8:50:26 PM
// Purpose: Definition of Class User

using System;
using Newtonsoft.Json;
using TechHealth.Core;

namespace TechHealth.Model
{
   
   public class User
   {
      public string Username { get; set; }


      public string Password { get; set; }

      public bool ShouldSerialize { get; set; }

      public bool ShouldSerializeUsername()
      {
         return ShouldSerialize;
      }
      public bool ShouldSerializePassword()
      {
         return ShouldSerialize;
      }
      
   }
}