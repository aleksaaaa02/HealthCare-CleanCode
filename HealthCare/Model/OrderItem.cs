﻿using HealthCare.Repository;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public class OrderItem : ISerializable, IKey
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
        public DateTime Scheduled { get; set; }
        public bool Executed { get; set; }

        public OrderItem() : this(0, 0, 0, DateTime.MinValue, false) { }

        public OrderItem(int id, int equipmentId, int quantity, DateTime scheduled, bool executed)
        {
            Id = id;
            EquipmentId = equipmentId;
            Quantity = quantity;
            Scheduled = scheduled;
            Executed = executed;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            EquipmentId = int.Parse(values[1]);
            Quantity = int.Parse(values[2]);
            Scheduled = DateTime.Parse(values[3]);
            Executed = bool.Parse(values[4]);
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), EquipmentId.ToString(), Quantity.ToString(), Scheduled.ToString(), Executed.ToString() };
        }

        public object GetKey()
        {
            return Id;
        }

        public void SetKey(object key)
        {
            Id = (int) key;
        }
    }
}
