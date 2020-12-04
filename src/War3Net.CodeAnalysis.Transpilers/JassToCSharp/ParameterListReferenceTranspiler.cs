﻿// ------------------------------------------------------------------------------
// <copyright file="ParameterListReferenceTranspiler.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace War3Net.CodeAnalysis.Transpilers
{
    public static partial class JassToCSharpTranspiler
    {
        public static IEnumerable<ParameterSyntax> Transpile(this Jass.Syntax.ParameterListReferenceSyntax parameterListReferenceNode, params TokenTranspileFlags[] flags)
        {
            _ = parameterListReferenceNode ?? throw new ArgumentNullException(nameof(parameterListReferenceNode));

            return parameterListReferenceNode.Select((node, index)
                => node.Transpile(
                    index + 1 > flags.Length
                    ? (TokenTranspileFlags)0
                    : flags[index]));
        }
    }
}