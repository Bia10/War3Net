﻿// ------------------------------------------------------------------------------
// <copyright file="FunctionCallTranspiler.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

#pragma warning disable SA1402 // File may only contain a single type
#pragma warning disable SA1649 // File name should match first type name

using System;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace War3Net.CodeAnalysis.Jass.Transpilers
{
    public static partial class JassToCSharpTranspiler
    {
        public static ExpressionSyntax Transpile(this Syntax.FunctionCallSyntax functionCallNode)
        {
            _ = functionCallNode ?? throw new ArgumentNullException(nameof(functionCallNode));

            var functionCall = SyntaxFactory.InvocationExpression(
                functionCallNode.IdentifierNameNode.TranspileExpression());

            if (functionCallNode.EmptyArgumentListNode is null)
            {
                return functionCall.AddArgumentListArguments(functionCallNode.ArgumentListNode.Transpile().ToArray());
            }

            return functionCall;
        }

        public static ExpressionSyntax Transpile(this Syntax.FunctionCallSyntax functionCallNode, out bool @const)
        {
            _ = functionCallNode ?? throw new ArgumentNullException(nameof(functionCallNode));

            @const = false;

            var functionCall = SyntaxFactory.InvocationExpression(
                functionCallNode.IdentifierNameNode.TranspileExpression());

            if (functionCallNode.EmptyArgumentListNode is null)
            {
                return functionCall.AddArgumentListArguments(functionCallNode.ArgumentListNode.Transpile().ToArray());
            }

            return functionCall;
        }
    }

    public static partial class JassToLuaTranspiler
    {
        public static void Transpile(this Syntax.FunctionCallSyntax functionCallNode, ref StringBuilder sb)
        {
            _ = functionCallNode ?? throw new ArgumentNullException(nameof(functionCallNode));

            functionCallNode.IdentifierNameNode.TranspileExpression(ref sb);
            sb.Append('(');
            if (functionCallNode.EmptyArgumentListNode is null)
            {
                functionCallNode.ArgumentListNode.Transpile(ref sb);
            }

            sb.Append(')');
        }
    }
}