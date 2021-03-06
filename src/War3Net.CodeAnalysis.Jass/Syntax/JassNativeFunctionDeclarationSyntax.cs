﻿// ------------------------------------------------------------------------------
// <copyright file="JassNativeFunctionDeclarationSyntax.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

namespace War3Net.CodeAnalysis.Jass.Syntax
{
    public class JassNativeFunctionDeclarationSyntax : IDeclarationSyntax
    {
        public JassNativeFunctionDeclarationSyntax(JassFunctionDeclaratorSyntax functionDeclarator)
        {
            FunctionDeclarator = functionDeclarator;
        }

        public JassFunctionDeclaratorSyntax FunctionDeclarator { get; init; }

        public bool Equals(IDeclarationSyntax? other)
        {
            return other is JassNativeFunctionDeclarationSyntax nativeFunctionDeclaration
                && FunctionDeclarator.Equals(nativeFunctionDeclaration.FunctionDeclarator);
        }

        public override string ToString() => $"{JassKeyword.Native} {FunctionDeclarator}";
    }
}