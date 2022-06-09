using System;
using System.Linq;

using Microsoft.CodeAnalysis;


namespace R5T.F0003
{
    public partial interface ISyntaxNodeOperator
    {
        /// <summary>
        /// Removes all trailing trivia from all descendant tokens and prepends it to the leading trivia of the following node.
        /// </summary>
        public TNode MoveDescendantTrailingTriviaToLeadingTrivia<TNode>(TNode node)
            where TNode : SyntaxNode
        {
            var descendantTokensWithTrailingTrivia = node.DescendantTokens()
                .Where(xToken => xToken.HasTrailingTrivia)
                // Make sure to evaluate now, so that tokens are not found as they are being annotated.
                .Now();

            node = node.AnnotateTokens_Untyped(
                descendantTokensWithTrailingTrivia,
                out var annotationsByToken);

            foreach (var tokenAnnotation in annotationsByToken.Values)
            {
                var originalToken = node.GetAnnotatedToken(tokenAnnotation);

                var token = originalToken;

                var originalNextToken = token.GetNextToken();

                var nextToken = originalNextToken;

                if (nextToken.IsNone())
                {
                    // Unable to move trailing trivia of last token to leading trivia of next token, since no next token exists!
                    continue;
                }

                var tokenTrailingTrivia = token.TrailingTrivia;

                token = token.WithoutTrailingTrivia();

                var nextTokenLeadingTrivia = nextToken.LeadingTrivia;

                var newNextTokenLeadingTrivia = nextTokenLeadingTrivia.Prepend(tokenTrailingTrivia);

                nextToken = nextToken.WithLeadingTrivia(newNextTokenLeadingTrivia);

                node = node.ReplaceTokens_Better(new[]
                {
                    (originalToken, token),
                    (originalNextToken, nextToken)
                });
            }

            return node;
        }
    }
}