﻿// ------------------------------------------------------------------------------
// <copyright file="ArgumentListTailSyntax.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace War3Net.CodeAnalysis.Jass.Syntax
{
    public sealed class ArgumentListTailSyntax : SyntaxNode, IEnumerable<NewExpressionSyntax>
    {
        private readonly List<CommaSeparatedExpressionSyntax>? _exprs;
        private readonly EmptyNode? _empty;

        public ArgumentListTailSyntax(params CommaSeparatedExpressionSyntax[] expressionNodes)
            : base(expressionNodes)
        {
            // TODO: check not null
            _exprs = new List<CommaSeparatedExpressionSyntax>(expressionNodes);
        }

        public ArgumentListTailSyntax(EmptyNode emptyNode)
            : base(emptyNode)
        {
            _empty = emptyNode ?? throw new ArgumentNullException(nameof(emptyNode));
        }

        public CommaSeparatedExpressionSyntax[] Arguments => _exprs?.ToArray() ?? Array.Empty<CommaSeparatedExpressionSyntax>();

        public IEnumerator<NewExpressionSyntax> GetEnumerator()
        {
            return (_exprs is not null
                ? _exprs.Select(node => node.ExpressionNode)
                : Enumerable.Empty<NewExpressionSyntax>())
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (_exprs is not null
                ? _exprs.Select(node => node.ExpressionNode)
                : Enumerable.Empty<NewExpressionSyntax>())
                .GetEnumerator();
        }

        internal sealed class Parser : ManyParser
        {
            private static Parser _parser;

            internal static Parser Get => _parser ?? (_parser = new Parser()).Init();

            protected override SyntaxNode CreateNode(SyntaxNode node)
            {
                if (node is EmptyNode emptyNode)
                {
                    return new ArgumentListTailSyntax(emptyNode);
                }
                else
                {
                    return new ArgumentListTailSyntax(node.GetChildren().Select(n => n as CommaSeparatedExpressionSyntax).ToArray());
                }
            }

            private Parser Init()
            {
                SetParser(CommaSeparatedExpressionSyntax.Parser.Get);

                return this;
            }
        }
    }
}