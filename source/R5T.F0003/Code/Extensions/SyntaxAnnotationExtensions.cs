using System;

using Microsoft.CodeAnalysis;


namespace System
{
    public static class SyntaxAnnotationExtensions
    {
        public static SyntaxNode GetNodeWithin(this SyntaxAnnotation annotation,
            SyntaxNode parentNode)
        {
            var output = parentNode.GetAnnotatedNode_Untyped(annotation);
            return output;
        }

        public static TNode GetNodeOfTypeWithin<TNode>(this SyntaxAnnotation annotation,
            SyntaxNode node)
            where TNode : SyntaxNode
        {
            var output = node.GetAnnotatedNodeOfType<TNode>(annotation);
            return output;
        }

        public static SyntaxToken GetTokenWithin(this SyntaxAnnotation annotation,
            SyntaxNode node)
        {
            var output = node.GetAnnotatedToken(annotation);
            return output;
        }

        public static SyntaxTrivia GetTriviaWithin(this SyntaxAnnotation annotation,
            SyntaxNode node)
        {
            var output = node.GetAnnotatedTriviaSingle(annotation);
            return output;
        }

        public static TNode ModifyToken<TNode>(this SyntaxAnnotation annotation,
            TNode node,
            Func<SyntaxToken, SyntaxToken> tokenModifer)
            where TNode : SyntaxNode
        {
            var originalToken = annotation.GetTokenWithin(node);

            var modifiedToken = tokenModifer(originalToken);

            var output = node.ReplaceToken_Better(originalToken, modifiedToken);
            return output;
        }
    }
}
