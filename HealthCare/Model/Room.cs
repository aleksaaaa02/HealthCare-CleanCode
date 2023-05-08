﻿using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public enum RoomType
    {
        Examinational,
        Operational,
        PatientCare,
        Reception,
        Warehouse
    }
    public class Room : ISerializable, IKey
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoomType Type { get; set; }
        public Room() : this(0, "", RoomType.Warehouse) { }
        public Room(int id, string name, RoomType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), Name, Type.ToString() };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Type = (RoomType) Enum.Parse(typeof(RoomType), values[2]);
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
