﻿// ------------------------------------------------------------------------------
// <copyright file="TypeTranspiler.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

#pragma warning disable SA1402 // File may only contain a single type
#pragma warning disable SA1649 // File name should match first type name

using System;
using System.Text;

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace War3Net.CodeAnalysis.Jass.Transpilers
{
    public static partial class JassToCSharpTranspiler
    {
        public static TypeSyntax Transpile(this Syntax.TypeSyntax typeNode, TokenTranspileFlags flags = (TokenTranspileFlags)0)
        {
            _ = typeNode ?? throw new ArgumentNullException(nameof(typeNode));

            return typeNode.TypeNameToken.TranspileType(flags);
        }
    }

    public static partial class JassToLuaTranspiler
    {
        public static void Transpile(this Syntax.TypeSyntax typeNode, ref StringBuilder sb)
        {
            // _ = typeNode ?? throw new ArgumentNullException(nameof(typeNode));

            throw new NotSupportedException();
        }
    }
}