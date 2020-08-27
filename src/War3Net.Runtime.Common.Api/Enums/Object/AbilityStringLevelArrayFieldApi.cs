﻿// ------------------------------------------------------------------------------
// <copyright file="AbilityStringLevelArrayFieldApi.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable SA1310 // Field names should not contain underscore
#pragma warning disable SA1401 // Fields should be private

using War3Net.Runtime.Common.Enums.Object;

namespace War3Net.Runtime.Common.Api.Enums.Object
{
    public static class AbilityStringLevelArrayFieldApi
    {
        public static AbilityStringLevelArrayField ConvertAbilityStringLevelArrayField(int i)
        {
            return AbilityStringLevelArrayField.GetAbilityStringLevelArrayField(i);
        }
    }
}