using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;

using Instances = R5T.F0003.Instances;


namespace System
{
    public static partial class SyntaxNodeExtensions
    {
        public static TNode AddLeadingLeadingTrivia<TNode>(this TNode node,
            IEnumerable<SyntaxTrivia> trivias)
            where TNode : SyntaxNode
        {
            var newLeadingTrivia = node.HasLeadingTrivia
                ? node.GetLeadingTrivia().AddLeadingTrivia(trivias)
                : new SyntaxTriviaList(trivias);

            var output = node.WithLeadingTrivia(newLeadingTrivia);
            return output;
        }

        public static TNode AddLeadingLeadingTrivia<TNode>(this TNode node,
            params SyntaxTrivia[] trivias)
            where TNode : SyntaxNode
        {
            var output = node.AddLeadingLeadingTrivia(trivias.AsEnumerable());
            return output;
        }

        public static TNode AddLeadingLeadingTrivia<TNode>(this TNode node,
            SyntaxTriviaList trivia)
            where TNode : SyntaxNode
        {
            var output = node.AddLeadingLeadingTrivia(trivia.AsEnumerable());
            return output;
        }

        public static TNode AddLeadingTrivia<TNode>(this TNode syntaxNode, params SyntaxTrivia[] trivia)
            where TNode : SyntaxNode
        {
            var output = syntaxNode.AddLeadingLeadingTrivia(trivia);
            return output;
        }

        public static TNode AddTrailingLeadingTrivia<TNode>(this TNode node,
            params SyntaxTrivia[] trivia)
            where TNode : SyntaxNode
        {
            var newLeadingTrivia = node.HasLeadingTrivia
                ? node.GetLeadingTrivia().AddTrailingTrivia(trivia)
                : new SyntaxTriviaList(trivia);

            var output = node.WithLeadingTrivia(newLeadingTrivia);
            return output;
        }

        public static TNode AddTrailingTrailingTrivia<TNode>(this TNode node,
            params SyntaxTrivia[] trivia)
            where TNode : SyntaxNode
        {
            var newTrailingTrivia = node.HasTrailingTrivia
                ? node.GetTrailingTrivia().AddTrailingTrivia(trivia)
                : new SyntaxTriviaList(trivia);

            var output = node.WithTrailingTrivia(newTrailingTrivia);
            return output;
        }

        public static TNode AddLeadingTrailingTrivia<TNode>(this TNode node,
            params SyntaxTrivia[] trivia)
            where TNode : SyntaxNode
        {
            var newTrailingTrivia = node.HasTrailingTrivia
                ? node.GetTrailingTrivia().AddLeadingTrivia(trivia)
                : new SyntaxTriviaList(trivia);

            var output = node.WithTrailingTrivia(newTrailingTrivia);
            return output;
        }

        public static TNode AddTrailingTrivia<TNode>(this TNode node,
            params SyntaxTrivia[] trivia)
            where TNode : SyntaxNode
        {
            var output = node.AddTrailingTrailingTrivia(trivia);
            return output;
        }

        public static IEnumerable<SyntaxTrivia> GetDescendantTrivias(this SyntaxNode node)
        {
            var output = node.DescendantTrivia();
            return output;
        }

        public static IEnumerable<SyntaxTrivia> GetDescendantTrivias_IncludingWithinStructuredTrivia(this SyntaxNode node)
        {
            var output = node.DescendantTrivia(descendIntoTrivia: true)
                .SelectMany(xTrivia =>
                {
                    if(xTrivia.HasStructure)
                    {
                        var structure = xTrivia.GetStructure();

                        var trivias = structure.GetDescendantTrivias_IncludingWithinStructuredTrivia();
                        return trivias;
                    }
                    else
                    {
                        return EnumerableHelper.From(xTrivia);
                    }
                })
                ;

            return output;
        }

        public static IEnumerable<SyntaxTrivia> GetEndOfLineTrivias(this SyntaxNode node)
        {
            var output = node.GetDescendantTrivias_IncludingWithinStructuredTrivia()
                .Where(xTrivia => xTrivia.IsEndOfLine())
                ;

            return output;
        }

        /// <inheritdoc cref="R5T.F0003.ISyntaxNodeOperator.MoveDescendantTrailingTriviaToLeadingTrivia{TNode}(TNode)"/>
        public static TNode MoveDescendantTrailingTriviaToLeadingTrivia<TNode>(this TNode node)
            where TNode : SyntaxNode
        {
            return Instances.SyntaxNodeOperator.MoveDescendantTrailingTriviaToLeadingTrivia(node);
        }

        public static TNode SetLeadingTrivia<TNode>(this TNode syntaxNode,
            SyntaxTriviaList trivia)
            where TNode : SyntaxNode
        {
            var output = syntaxNode.WithLeadingTrivia(trivia);
            return output;
        }

        public static TNode SetLeadingTrivia<TNode>(this TNode syntaxNode,
            params SyntaxTrivia[] trivia)
            where TNode : SyntaxNode
        {
            var output = syntaxNode.WithLeadingTrivia(trivia);
            return output;
        }
    }
}
