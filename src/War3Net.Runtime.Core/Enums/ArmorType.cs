﻿// ------------------------------------------------------------------------------
// <copyright file="ArmorType.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using War3Net.Runtime.Core;

namespace War3Net.Runtime.Enums
{
    public sealed class ArmorType : Handle
    {
        private static readonly Dictionary<int, ArmorType> _types = GetTypes().ToDictionary(t => (int)t, t => new ArmorType(t));

        private readonly Type _type;

        private ArmorType(Type type)
        {
            _type = type;
        }

        public enum Type
        {
            Undefined = 0,
            Flesh = 1,
            Metal = 2,
            Wood = 3,
            Ethereal = 4,
            Stone = 5,
        }

        public static implicit operator Type(ArmorType armorType) => armorType._type;

        public static explicit operator int(ArmorType armorType) => (int)armorType._type;

        public static ArmorType GetArmorType(int i)
        {
            if (!_types.TryGetValue(i, out var armorType))
            {
                armorType = new ArmorType((Type)i);
                _types.Add(i, armorType);
            }

            return armorType;
        }

        private static IEnumerable<Type> GetTypes()
        {
            foreach (Type type in Enum.GetValues(typeof(Type)))
            {
                yield return type;
            }
        }
    }
}