﻿// ------------------------------------------------------------------------------
// <copyright file="JassCompilationUnitSyntax.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Immutable;
using System.Linq;

namespace War3Net.CodeAnalysis.Jass.Syntax
{
    public class JassCompilationUnitSyntax : IEquatable<JassCompilationUnitSyntax>
    {
        public JassCompilationUnitSyntax(ImmutableArray<IDeclarationSyntax> declarations)
        {
            Declarations = declarations;
        }

        public ImmutableArray<IDeclarationSyntax> Declarations { get; init; }

        public bool Equals(JassCompilationUnitSyntax? other)
        {
            return other is not null
                && Declarations.SequenceEqual(other.Declarations);
        }
    }
}