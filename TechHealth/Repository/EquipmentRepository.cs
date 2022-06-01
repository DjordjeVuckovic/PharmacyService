﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechHealth.Model;

namespace TechHealth.Repository
{
    public class EquipmentRepository : GenericRepository<string, Equipment>
    {
        private static readonly EquipmentRepository instance = new EquipmentRepository();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static EquipmentRepository()
        {
        }

        private EquipmentRepository()
        {
        }

        public static EquipmentRepository Instance => instance;
        protected override string GetKey(Equipment entity)
        {
            return entity.id;
        }

        protected override string GetPath()
        {
            return @"../../Json/equipment.json";
        }

        protected override void RemoveAllReference(string key)
        {
            throw new NotImplementedException();
        }

        protected override void ShouldSerialize(Equipment entity)
        {
            //skip
        }

        //public List<String> GetEqNames()
        //{
        //    List<String> eqNames = new List<String>();

        //    foreach (var eq in GetAllToList())
        //    {
        //        eqNames.Add(eq.name);
        //    }
        //    return eqNames;
        //}

        //public int GetEqIndex(string eqName, List<Equipment> eqList)
        //{
        //    int index = 0;
        //    for (int i = 0; i < eqList.Count; i++)
        //    {
        //        if (eqList[i].name == eqName)
        //        {
        //            index = i;
        //            break;
        //        }
        //        i++;
        //    }
        //    return index;
        //}
    }
}
