using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;


namespace System
{
    public static partial class SyntaxNodeExtensions
    {
        public static bool IsFirstNodeInCompilationUnit(this SyntaxNode node)
        {
            var firstToken = node.GetFirstToken();

            var output = firstToken.IsFirstTokenInCompilationUnit();
            return output;
        }

        public static SyntaxList<TNode> ToSyntaxList<TNode>(this IEnumerable<TNode> nodes)
            where TNode : SyntaxNode
        {
            var output = new SyntaxList<TNode>(nodes);
            return output;
        }
    }
}
