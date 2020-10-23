﻿// ------------------------------------------------------------------------------
// <copyright file="SetStatementTranspiler.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

#pragma warning disable SA1402 // File may only contain a single type
#pragma warning disable SA1649 // File name should match first type name

using System;
using System.Text;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace War3Net.CodeAnalysis.Jass.Transpilers
{
    public static partial class JassToCSharpTranspiler
    {
        public static StatementSyntax Transpile(this Syntax.SetStatementSyntax setStatementNode)
        {
            _ = setStatementNode ?? throw new ArgumentNullException(nameof(setStatementNode));

            return SyntaxFactory.ExpressionStatement(SyntaxFactory.AssignmentExpression(
                Microsoft.CodeAnalysis.CSharp.SyntaxKind.SimpleAssignmentExpression,
                setStatementNode.ArrayIndexerNode is null
                    ? setStatementNode.IdentifierNameNode.TranspileExpression()
                    : SyntaxFactory.ElementAccessExpression(
                        setStatementNode.IdentifierNameNode.TranspileExpression(),
                        setStatementNode.ArrayIndexerNode.Transpile()),
                setStatementNode.EqualsValueClauseNode.ValueNode.Transpile()));
        }
    }

    public static partial class JassToLuaTranspiler
    {
        public static void Transpile(this Syntax.SetStatementSyntax setStatementNode, ref StringBuilder sb)
        {
            _ = setStatementNode ?? throw new ArgumentNullException(nameof(setStatementNode));

            setStatementNode.IdentifierNameNode.TranspileExpression(ref sb);
            if (setStatementNode.EmptyArrayIndexerNode is null)
            {
                setStatementNode.ArrayIndexerNode.Transpile(ref sb);
            }

            setStatementNode.EqualsValueClauseNode.Transpile(ref sb);
        }
    }
}