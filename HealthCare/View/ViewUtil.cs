﻿using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HealthCare.View
{
    internal static class ViewUtil
    {
        public static string Translate(bool b)
        {
            if (b) return "da";
            return "ne";
        }

        public static string Translate(Gender gender)
        {
            switch (gender)
            {
                case Gender.Female:
                    return "žensko";
                default:
                    return "muško";
            }
        }

        public static string Translate(EquipmentType type)
        {
            switch(type)
            {
                case EquipmentType.Examinational:
                    return "za preglede";
                case EquipmentType.Operational:
                    return "operaciona";
                case EquipmentType.RoomFurniture:
                    return "sobni namestaj";
                default:
                    return "oprema za hodnike";
            }
        }

        public static string Translate(RoomType type)
        {
            switch (type)
            {
                case RoomType.Examinational:
                    return "za preglede";
                case RoomType.Operational:
                    return "operaciona";
                case RoomType.PatientCare:
                    return "smestaj bolesnika";
                case RoomType.Reception:
                    return "recepcija";
                default:
                    return "magacin";
            }
        }
    }
}
