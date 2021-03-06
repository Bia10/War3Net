﻿// ------------------------------------------------------------------------------
// <copyright file="FogState.cs" company="Drake53">
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
    public sealed class FogState : Handle
    {
        private static readonly Dictionary<int, FogState> _states = GetTypes().ToDictionary(t => (int)t, t => new FogState(t));

        private readonly Type _type;

        private FogState(Type type)
        {
            _type = type;
        }

        [Flags]
        public enum Type
        {
            Masked = 1 << 0,
            Fogged = 1 << 1,
            Visible = 1 << 2,
        }

        public static implicit operator Type(FogState fogState) => fogState._type;

        public static explicit operator int(FogState fogState) => (int)fogState._type;

        public static FogState GetFogState(int i)
        {
            if (!_states.TryGetValue(i, out var fogState))
            {
                fogState = new FogState((Type)i);
                _states.Add(i, fogState);
            }

            return fogState;
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